using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegGame.Models
{
    public class PegMove
    {
        public PegHole FromHole { get; set; }
        public PegHole ToHole { get; set; }
        public PegHole JumpHole { get; set; }

        public override string ToString()
        {
            return $"From: {this.FromHole.Label}, To: {this.ToHole.Label}, Jump: {this.JumpHole.Label}\r\n";
        }
    }
}
