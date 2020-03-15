using PegGame.Models;
using System;
using System.Collections.Generic;

namespace PegGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read any previous boards
            List<PegBoard> PriorBoards = GetPriorBoards();

            // Create list of legal moves
            List<PegMove> LegalMoves = GetLegalMoves();

            bool done = false;
            while (!done)
            {
                // Create new board
                PegBoard board = new PegBoard();
                board.LegalMoves = LegalMoves;
                board.PriorBoards = PriorBoards;

                // Play Game
                PegBoard.Outcome outcome = board.PlayGame();

                // if game lost or number of moves greater than ten, save board
                if (outcome != PegBoard.Outcome.Champ)
                {
                    PriorBoards.Add(board);
                }
                else
                    done = true;

                // display the board moves
                foreach(var move in board.Moves)
                {

                }
            }

        }

        private static List<PegBoard> GetPriorBoards()
        {
            // read any prior boards from persistent storage
            throw new NotImplementedException();
        }

        // Build list of legal moves on a given board
        private static List<PegMove> GetLegalMoves()
        {
            List<PegMove> list = new List<PegMove>();
            list.Add(new PegMove() { FromHole = 1, ToHole = 4, JumpHole = 2 });
            list.Add(new PegMove() { FromHole = 2, ToHole = 7, JumpHole = 4 });
            list.Add(new PegMove() { FromHole = 2, ToHole = 9, JumpHole = 5 });
            list.Add(new PegMove() { FromHole = 3, ToHole = 8, JumpHole = 5 });
            list.Add(new PegMove() { FromHole = 3, ToHole = 10, JumpHole = 6 });

            list.Add(new PegMove() { FromHole = 4, ToHole = 1, JumpHole = 2 });
            list.Add(new PegMove() { FromHole = 4, ToHole = 6, JumpHole = 5 });
            list.Add(new PegMove() { FromHole = 4, ToHole = 11, JumpHole = 7 });
            list.Add(new PegMove() { FromHole = 4, ToHole = 13, JumpHole = 8 });
            list.Add(new PegMove() { FromHole = 5, ToHole = 12, JumpHole = 8 });
            list.Add(new PegMove() { FromHole = 5, ToHole = 14, JumpHole = 9 });

            list.Add(new PegMove() { FromHole = 6, ToHole = 1, JumpHole = 3 });
            list.Add(new PegMove() { FromHole = 6, ToHole = 4, JumpHole = 5 });
            list.Add(new PegMove() { FromHole = 6, ToHole = 13, JumpHole = 9 });
            list.Add(new PegMove() { FromHole = 6, ToHole = 15, JumpHole = 10 });
            list.Add(new PegMove() { FromHole = 7, ToHole = 2, JumpHole = 4 });
            list.Add(new PegMove() { FromHole = 7, ToHole = 9, JumpHole = 8 });

            list.Add(new PegMove() { FromHole = 8, ToHole = 3, JumpHole = 5 });
            list.Add(new PegMove() { FromHole = 8, ToHole = 10, JumpHole = 9 });
            list.Add(new PegMove() { FromHole = 9, ToHole = 2, JumpHole = 5 });
            list.Add(new PegMove() { FromHole = 9, ToHole = 7, JumpHole = 8 });
            list.Add(new PegMove() { FromHole = 10, ToHole = 3, JumpHole = 6 });
            list.Add(new PegMove() { FromHole = 10, ToHole = 8, JumpHole = 9 });

            list.Add(new PegMove() { FromHole = 11, ToHole = 4, JumpHole = 7 });
            list.Add(new PegMove() { FromHole = 11, ToHole = 13, JumpHole = 12 });
            list.Add(new PegMove() { FromHole = 12, ToHole = 5, JumpHole = 8 });
            list.Add(new PegMove() { FromHole = 12, ToHole = 14, JumpHole = 13 });

            list.Add(new PegMove() { FromHole = 13, ToHole = 4, JumpHole = 8 });
            list.Add(new PegMove() { FromHole = 13, ToHole = 6, JumpHole = 9 });
            list.Add(new PegMove() { FromHole = 14, ToHole = 12, JumpHole = 13 });
            list.Add(new PegMove() { FromHole = 14, ToHole = 5, JumpHole = 9 });
            list.Add(new PegMove() { FromHole = 15, ToHole = 6, JumpHole = 10 });
            list.Add(new PegMove() { FromHole = 15, ToHole = 13, JumpHole = 14 });

            return list;
        }
    }
}
