using System;
using System.Collections.Generic;
using System.Linq;

namespace PegGame.Models
{
    public class PegBoard
    {
        public List<PegHole> PegHoles { get; set; }
        public List<PegMove> Moves { get; set; }
        public List<LegalMove> LegalMoves { get; set; }
        public List<BoardImage> PriorBoards { get; set; }

        public enum Outcome
        {
            Lost = 0,
            Win = 1,
            Champ = 2
        };

        public PegBoard()
        {
            // build the new board - list of holes - all but one occupied
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
                List<PegMove> availableMoves = GetAvailableMoves();

                // if no move available, game over
                if (availableMoves.Count < 1)
                {
                    done = true;
                    break;
                }

                // TODO: pick next move at random
                PegMove nextMove = GetNextMove(availableMoves);
                if(nextMove != null)
                {
                    // make move
                    MovePeg(this.PegHoles, nextMove);

                    // save the move
                    this.Moves.Add(nextMove);
                }
                // if nextMove is null 

                // if there is a single peg remaining, game won
                if (this.PegHoles.Where(h => h.Occupied).Count() == 1)
                {
                    retVal = Outcome.Win;

                    // if game won and number of moves less than ten, game champ
                    if (this.Moves.Count < 10)
                        retVal = Outcome.Champ;

                    done = true;
                }
            }
            return retVal;
        }

        public int PegsRemaining()
        {
            return this.PegHoles.Where(h => h.Occupied).Count();
        }

        // choose the next move randomly from list
        private PegMove GetNextMove(List<PegMove> availableMoves)
        {
            PegMove retVal = null;

            List<int> alreadyChecked = new List<int>();
            var random = new Random();
            while(alreadyChecked.Count < availableMoves.Count)
            {
                int idx = random.Next(availableMoves.Count);
                if (!alreadyChecked.Contains(idx))
                {
                    retVal = availableMoves[idx];
                    break;
                }
            }
            return retVal;
        }

        // return a list of legal moves that can be made on current board
        private List<PegMove> GetAvailableMoves()
        {
            List<PegMove> availableMoves = new List<PegMove>();

            // iterate thru list of all holes
            foreach (var hole in this.PegHoles)
            {
                if (hole.Occupied)
                {
                    foreach (var legalMove in LegalMoves)
                    {
                        if (hole.Label == legalMove.FromHoleLabel)
                        {
                            // get the data for to and jump holes
                            var toHole = this.PegHoles.Where(h => h.Label == legalMove.ToHoleLabel).FirstOrDefault();
                            var jumpHole = this.PegHoles.Where(h => h.Label == legalMove.JumpHoleLabel).FirstOrDefault();

                            // if the hole is occupied and the jump hole is occupied and the to hole is not occupied - available move
                            if (toHole.Occupied == false && jumpHole.Occupied == true)
                            {
                                var goodMove = new PegMove();
                                goodMove.FromHole = hole;
                                goodMove.ToHole = toHole;
                                goodMove.JumpHole = jumpHole;
                                availableMoves.Add(goodMove);
                            }
                        }
                    }
                }
            }
            return availableMoves;
        }

        // compare the board after the move to prior boards
        private bool IsMoveGood(PegMove move)
        {
            bool retVal = true;

            // create a copy of the existing board
            List<PegHole> currHoles = new List<PegHole>();
            foreach (var hole in this.PegHoles)
                currHoles.Add(hole);

            // make the move on the current board
            MovePeg(currHoles, move);

            foreach (var board in PriorBoards)
            {
                // compare the list of holes to prior boards
                if (IsBoardMatch(board.PegHoles, currHoles))
                {
                    // board matches prior board, return not good move (false)
                    retVal = false;
                    break;
                }
            }
            return retVal;
        }

        private void MovePeg(List<PegHole> holes, PegMove move)
        {
            // update the list of holes according to the move
            var fromHole = holes.Where(h => h.Label == move.FromHole.Label).First();
            fromHole.Occupied = false;

            var toHole = holes.Where(h => h.Label == move.ToHole.Label).First();
            toHole.Occupied = true;

            var jumpHole = holes.Where(h => h.Label == move.JumpHole.Label).First();
            jumpHole.Occupied = false;
        }

        private bool IsBoardMatch(List<PegHole> pegHoles, List<PegHole> currHoles)
        {
            bool match = true;
            int idx = 1;
            while (idx <= 15)
            {
                var holeA = pegHoles[idx - 1];
                var holeB = currHoles[idx - 1];

                if (holeA.Occupied != holeB.Occupied)
                {
                    match = false;
                    break;
                }
                idx++;
            }
            return match;
        }
    }
}
