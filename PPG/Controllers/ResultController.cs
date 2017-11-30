using Microsoft.AspNetCore.Mvc;
using PPG.Data;
using PPG.Models.Entities;
using PPG.Models.Entities.Count;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PPG.Controllers
{
    public class ResultController: Controller
    {
        private ElectContext electContext;

        private Config config;

        public ResultController(ElectContext electContext, Config config)
        {
            this.config = config;
            this.electContext = electContext;
        }

        [HttpGet("election-results")]
        public ViewResult ElectionResults()
        {
            string serializedObjectData = JsonConvert.SerializeObject(config.ADMIN);
            var serializedElections = getElectionsFromSpg(serializedObjectData, "api/spg/get-all-elections/");
            List<Election> elections = JsonConvert.DeserializeObject<List<Election>>(serializedElections);

            var electionResults = electContext.Votes.GroupBy(v => new { v.CandidateId, v.ElectionId }).Select(g => new { candidateId = g.Key.CandidateId, electionId = g.Key.ElectionId, count = g.Count() }).ToList();
            Dictionary<string, List<CandidateCount>> electionCounts = new Dictionary<string, List<CandidateCount>>();
            foreach(var electionResult in electionResults)
            {
                string electionName = getElectionNameById(electionResult.electionId, elections);
                string candidateFIO = getCandidateFioByElectionId(electionResult.electionId, electionResult.candidateId, elections);
                CandidateCount candidateCount = new CandidateCount(candidateFIO, electionResult.count);
                if (electionCounts.ContainsKey(electionName))
                {
                    electionCounts[electionName].Add(candidateCount);
                }                
                else
                {
                    List<CandidateCount> candidateCountList = new List<CandidateCount>();
                    candidateCountList.Add(candidateCount);
                    electionCounts[electionName] = candidateCountList;
                }
            }

            return View("ElectionResults", electionCounts);
        }

        private string getElectionsFromSpg(string serializedObjectData, string actioUri)
        {
            var client = new HttpClient();
            var content = new StringContent(serializedObjectData, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(config.SPG_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsync(actioUri, content).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                return responseString;
            }
            return null;
        }

        private string getElectionNameById(int electionId, List<Election> elections)
        {
            Election election = elections.Where(e => e.ID == electionId).SingleOrDefault();
            return election.Name;
        }

        private string getCandidateFioByElectionId(int electionId, int candidateId, List<Election> elections)
        {
            Election election = elections.Where(e => e.ID == electionId).SingleOrDefault();
            Candidate candidate = election.Candidates.Where(c => c.ID == candidateId).SingleOrDefault();
            return candidate.FIO;
        }
    }
}
