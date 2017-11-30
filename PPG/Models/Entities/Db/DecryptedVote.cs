using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG.Models.Entities
{
    public class DecryptedVote
    {
        public int ID { get; set; }
        public int ElectionId { get; set; }
        public int CandidateId { get; set; }
    }
}
