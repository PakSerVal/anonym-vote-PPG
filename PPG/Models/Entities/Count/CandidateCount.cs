using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG.Models.Entities.Count
{
    public class CandidateCount
    {
        public string CandidateFIO { get; set; }

        public int Count { get; set; }

        public CandidateCount(string CandidateFIO, int Count)
        {
            this.CandidateFIO = CandidateFIO;
            this.Count = Count;
        }
    }
}
