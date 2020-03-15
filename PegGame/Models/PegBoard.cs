using System;
using System.Collections.Generic;

namespace PegGame.Models
{
    public class PegBoard
    {
        public List<PegHole> PegHoles { get; set; }
        public List<PegHole> CurrentHoles { get; set; }
        public List<PegMove> Moves { get; set; }
        public List<PegMove> LegalMoves { get; set; }
        public List<PegBoard> PriorBoards { get; set; }

        public enum Outcome
        {
            Lost = 0,
            Win = 1,
            Champ = 2
        };

        public PegBoard()
        {
            // build list of holes - all but one occupied
            this.PegHoles = new List<PegHole>();
            PegHoles.Add(new PegHole() { Label = 1, Occupied = false });
            PegHoles.Add(new PegHole() { Label = 2, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 3, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 4, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 5, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 6, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 7, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 8, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 9, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 10, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 11, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 12, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 13, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 14, Occupied = true });
            PegHoles.Add(new PegHole() { Label = 15, Occupied = true });
        }

        public Outcome PlayGame()
        {
            bool done = false;
            Outcome retVal = Outcome.Lost;
            this.Moves = new List<PegMove>();

            while (!done)
            {
                // get list of legal moves
                List<PegMove> legalMoves = GetLegalMoves();

                // decide what move to make
                foreach (var move in legalMoves)
                {
                    // compare valid moves to prior boards
                    bool goodMove = IsMoveGood(move);
                    if (goodMove)
                    {
                        // make move
                        PegMove pegMove = MovePeg(this.PegHoles, move);

                        // save the move
                        this.Moves.Add(pegMove);

                        // break out of loop
                        break;
                    }
                }

                // if there is a single peg remaining, game won
                if (PegsRemaining() == 1)
                {
                    retVal = Outcome.Win;

                    // if game won and number of moves less than ten, game champ
                    if (this.Moves.Count < 10)
                        retVal = Outcome.Champ;
                }
            }
            return retVal;
        }

        private List<PegMove> GetLegalMoves()
        {
            List<PegMove> availableMove = new List<PegMove>();

            // iterate thru list of all holes
            foreach (var hole in this.PegHoles)
            {
                if (hole.Occupied)
                {
                    foreach (var move in LegalMoves)
                    {
                        // if the hole is occupied and the jump hole is occupied and the to hole is not occupied - available move
                        if (
                            move.FromHole.Label == hole.Label 
                            && move.JumpHole.Occupied == true 
                            && move.ToHole.Occupied == false
                            )
                        {
                            availableMove.Add(move);
                        }
                    }
                }
            }
            return availableMove;
        }

        // compare the board after the move to prior boards
        private bool IsMoveGood(PegMove move)
        {
            // create a copy of the existing board
            this.CurrentHoles = new List<PegHole>();
            foreach (var hole in this.PegHoles)
                this.CurrentHoles.Add(hole);

            // make the move on the current board

            foreach(var board in PriorBoards)
            {
                // compare the list of holes to prior boards

            }
            throw new NotImplementedException();
        }

        private PegMove MovePeg(List<PegHole> holes, PegMove move)
        {
            // update the holes according to the move
            throw new NotImplementedException();
        }

        private int PegsRemaining()
        {
            // how many holes are occupied
            throw new NotImplementedException();
        }
    }
}
