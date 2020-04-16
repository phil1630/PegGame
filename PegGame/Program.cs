using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PegGame.Models;

// 
// TODO:
// - Store the board image array to disk, read on startup
// 

namespace PegGame
{
    class Program
    {
        public static string PriorBoardFile = "PriorBoards.json";
        private static readonly log4net.ILog logger =
                    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static int maxGames = 50000;

        static void Main(string[] args)
        {
            if(args[0] != null)
            {
                // input of number of games to play
                maxGames = Int32.Parse(args[0]);
            }

            // Read any previous boards
            List<BoardImage> PriorBoards = GetSavedBoarImages();

            // Create list of legal moves
            List<LegalMove> LegalMoves = GetLegalMoves();

            bool done = false;
            int count = 0;
            while (!done)
            {
                // Create new board
                PegBoard board = new PegBoard();
                board.LegalMoves = LegalMoves;
                board.PriorBoards = PriorBoards;

                // Play Game
                //logger.Info("**New Game**");
                PegBoard.Outcome outcome = board.PlayGame();

                // add the board to the array of images
                PriorBoards.Add(BuildBoardImage(board, PriorBoards.Count + 1, outcome));

                // if game lost or number of moves greater than ten, save board
                if (outcome == PegBoard.Outcome.Champ)
                {
                    done = true;
                }

                // display the board moves
                //logger.Info("Board moves:");
                //foreach (var move in board.Moves)
                //{
                //    logger.Info(move.ToString());
                //}
                logger.Info($"Game: {count + 1} Outcome : {outcome.ToString()} Pegs Remaining: {board.PegsRemaining()}");

                count++;
                if (count == maxGames)
                    done = true;
            }
            // save the collection of boards
            SaveBoardImages(PriorBoards);
        }

        private static BoardImage BuildBoardImage(PegBoard board, int arrSize, PegBoard.Outcome outcome)
        {
            BoardImage boardImage = new BoardImage();
            boardImage.BoardId = arrSize.ToString("0000#");
            boardImage.Outcome = outcome.ToString();
            boardImage.PegHoles = board.PegHoles;

            // list of moves
            List<string> moveList = new List<string>();
            foreach (var move in board.Moves)
            {
                moveList.Add(move.ToString());
            }
            boardImage.Moves = moveList;
            return boardImage;
        }

        private static void SaveBoardImages(List<BoardImage> priorBoards)
        {
            // TODO: save the array of board images to json file
            string output = JsonConvert.SerializeObject(priorBoards);
            File.WriteAllText(PriorBoardFile, output);
        }

        private static List<BoardImage> GetSavedBoarImages()
        {
            List<BoardImage> list = new List<BoardImage>();
            if (File.Exists(PriorBoardFile))
            {
                // read any prior boards from persistent storage
                string json = File.ReadAllText(PriorBoardFile);
                list = JsonConvert.DeserializeObject<List<BoardImage>>(json);
            }
            return list;
        }

        // Build list of legal moves on a given board
        private static List<LegalMove> GetLegalMoves()
        {
            List<LegalMove> list = new List<LegalMove>();
            list.Add(new LegalMove() { FromHoleLabel = 1, ToHoleLabel = 4, JumpHoleLabel = 2 });
            list.Add(new LegalMove() { FromHoleLabel = 2, ToHoleLabel = 7, JumpHoleLabel = 4 });
            list.Add(new LegalMove() { FromHoleLabel = 2, ToHoleLabel = 9, JumpHoleLabel = 5 });
            list.Add(new LegalMove() { FromHoleLabel = 3, ToHoleLabel = 8, JumpHoleLabel = 5 });
            list.Add(new LegalMove() { FromHoleLabel = 3, ToHoleLabel = 10, JumpHoleLabel = 6 });

            list.Add(new LegalMove() { FromHoleLabel = 4, ToHoleLabel = 1, JumpHoleLabel = 2 });
            list.Add(new LegalMove() { FromHoleLabel = 4, ToHoleLabel = 6, JumpHoleLabel = 5 });
            list.Add(new LegalMove() { FromHoleLabel = 4, ToHoleLabel = 11, JumpHoleLabel = 7 });
            list.Add(new LegalMove() { FromHoleLabel = 4, ToHoleLabel = 13, JumpHoleLabel = 8 });
            list.Add(new LegalMove() { FromHoleLabel = 5, ToHoleLabel = 12, JumpHoleLabel = 8 });
            list.Add(new LegalMove() { FromHoleLabel = 5, ToHoleLabel = 14, JumpHoleLabel = 9 });

            list.Add(new LegalMove() { FromHoleLabel = 6, ToHoleLabel = 1, JumpHoleLabel = 3 });
            list.Add(new LegalMove() { FromHoleLabel = 6, ToHoleLabel = 4, JumpHoleLabel = 5 });
            list.Add(new LegalMove() { FromHoleLabel = 6, ToHoleLabel = 13, JumpHoleLabel = 9 });
            list.Add(new LegalMove() { FromHoleLabel = 6, ToHoleLabel = 15, JumpHoleLabel = 10 });
            list.Add(new LegalMove() { FromHoleLabel = 7, ToHoleLabel = 2, JumpHoleLabel = 4 });
            list.Add(new LegalMove() { FromHoleLabel = 7, ToHoleLabel = 9, JumpHoleLabel = 8 });

            list.Add(new LegalMove() { FromHoleLabel = 8, ToHoleLabel = 3, JumpHoleLabel = 5 });
            list.Add(new LegalMove() { FromHoleLabel = 8, ToHoleLabel = 10, JumpHoleLabel = 9 });
            list.Add(new LegalMove() { FromHoleLabel = 9, ToHoleLabel = 2, JumpHoleLabel = 5 });
            list.Add(new LegalMove() { FromHoleLabel = 9, ToHoleLabel = 7, JumpHoleLabel = 8 });
            list.Add(new LegalMove() { FromHoleLabel = 10, ToHoleLabel = 3, JumpHoleLabel = 6 });
            list.Add(new LegalMove() { FromHoleLabel = 10, ToHoleLabel = 8, JumpHoleLabel = 9 });

            list.Add(new LegalMove() { FromHoleLabel = 11, ToHoleLabel = 4, JumpHoleLabel = 7 });
            list.Add(new LegalMove() { FromHoleLabel = 11, ToHoleLabel = 13, JumpHoleLabel = 12 });
            list.Add(new LegalMove() { FromHoleLabel = 12, ToHoleLabel = 5, JumpHoleLabel = 8 });
            list.Add(new LegalMove() { FromHoleLabel = 12, ToHoleLabel = 14, JumpHoleLabel = 13 });

            list.Add(new LegalMove() { FromHoleLabel = 13, ToHoleLabel = 4, JumpHoleLabel = 8 });
            list.Add(new LegalMove() { FromHoleLabel = 13, ToHoleLabel = 6, JumpHoleLabel = 9 });
            list.Add(new LegalMove() { FromHoleLabel = 14, ToHoleLabel = 12, JumpHoleLabel = 13 });
            list.Add(new LegalMove() { FromHoleLabel = 14, ToHoleLabel = 5, JumpHoleLabel = 9 });
            list.Add(new LegalMove() { FromHoleLabel = 15, ToHoleLabel = 6, JumpHoleLabel = 10 });
            list.Add(new LegalMove() { FromHoleLabel = 15, ToHoleLabel = 13, JumpHoleLabel = 14 });

            return list;
        }
    }
}
