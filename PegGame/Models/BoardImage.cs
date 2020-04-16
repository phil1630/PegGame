using System;
using System.Collections.Generic;

namespace PegGame.Models
{
    public class BoardImage
    {
        public string BoardId { get; set; }
        public string Outcome { get; set; }
        public List<PegHole> PegHoles { get; set; }
        public List<string> Moves { get; set; }
    }
}
