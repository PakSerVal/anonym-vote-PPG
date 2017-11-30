using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PPG.Models.Entities
{
    public class Election
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<Candidate> Candidates { get; set; }
    }
}
