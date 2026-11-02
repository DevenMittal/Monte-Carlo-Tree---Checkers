namespace Monte_Carlo_Tree___Checkers
{
    enum Square
    {
       Up = 1,
       Down = 2,
       AI = 4,

       King = Up | Down,

       AIPiece = AI | Down,
       AIKing = AI | King,

       MePiece = Up,
       MeKing = King,
        


       Empty = 0,
       Player = 0
        

    }
    internal class Program
    {
        
        static void Main(string[] args)
        {
            //Helpful Query


            //for long timings, maybe I keep regenerating boards in terms of size(maybe use struct instead of board) indexer to access indexes blike array. Instead of reducing objects, make them easier to manage.
            Square[][] inputs = new Square[8][];
            inputs[0] = new Square[] { Square.Empty, Square.AIPiece, Square.Empty, Square.AIPiece, Square.Empty, Square.AIPiece, Square.Empty, Square.AIPiece };
            inputs[1] = new Square[] { Square.AIPiece, Square.Empty, Square.AIPiece, Square.Empty, Square.AIPiece, Square.Empty, Square.AIPiece, Square.Empty };
            inputs[2] = new Square[] { Square.Empty, Square.AIPiece, Square.Empty, Square.AIPiece, Square.Empty, Square.AIPiece, Square.Empty, Square.AIPiece };
            inputs[3] = new Square[] { Square.Empty, Square.Empty, Square.Empty, Square.Empty, Square.Empty, Square.Empty, Square.Empty, Square.Empty };
            inputs[4] = new Square[] { Square.Empty, Square.Empty, Square.Empty, Square.Empty, Square.Empty, Square.Empty, Square.Empty, Square.Empty };
            inputs[5] = new Square[] { Square.MePiece, Square.Empty, Square.MePiece, Square.Empty, Square.MePiece, Square.Empty, Square.MePiece, Square.Empty };
            inputs[6] = new Square[] { Square.Empty, Square.MePiece, Square.Empty, Square.MePiece, Square.Empty, Square.MePiece, Square.Empty, Square.MePiece };
            inputs[7] = new Square[] { Square.MePiece, Square.Empty, Square.MePiece, Square.Empty, Square.MePiece, Square.Empty, Square.MePiece, Square.Empty };


           

            Checkers root = new Checkers(inputs, Square.Player);

            bool demo = args.Any(argument => argument.Equals("--demo", StringComparison.OrdinalIgnoreCase));
            Play game = new Play(root, demo ? new Random(20261101) : new Random());

            if (demo)
            {
                Console.WriteLine("Monte Carlo Checkers demo");
                Console.WriteLine("Initial board");
                PrintBoard(game.rootTemp);
                var demoMoves = new[] { ((5, 0), (4, 1)), ((4, 1), (3, 0)), ((5, 2), (4, 3)) };
                foreach (var demoMove in demoMoves)
                {
                    Console.WriteLine($"Human move: {demoMove.Item1} -> {demoMove.Item2}");
                    bool moved = game.Move(demoMove.Item1, demoMove.Item2);
                    Console.WriteLine(moved ? "AI response after 1,600 MCTS iterations" : "Move rejected");
                    PrintBoard(game.rootTemp);
                }
                return;
            }

            while (game.IsGameOver() == State.StillPlaying)
            {
                Console.Clear();
                PrintBoard(game.rootTemp);
                if (!TryReadMove(out var piece, out var move))
                {
                    Console.WriteLine("Input ended. Exiting.");
                    return;
                }
                if (!game.Move(piece, move)) Console.WriteLine("Move rejected.");


            }
            Console.Clear();
            if (game.IsGameOver() == State.IWin)
            {
                Console.WriteLine("You Win!");
            }
            if (game.IsGameOver() == State.Tie)
            {
                Console.WriteLine("You Tied");
            }
            if (game.IsGameOver() == State.AIWin)
            {
                Console.WriteLine("You Lose");
            }

        }

        static bool TryReadMove(out (int x, int y) piece, out (int x, int y) move)
        {
            piece = default;
            move = default;
            int[] values = new int[4];
            for (int i = 0; i < values.Length; i++)
            {
                if (!int.TryParse(Console.ReadLine(), out values[i])) return false;
            }
            piece = (values[0], values[1]);
            move = (values[2], values[3]);
            return true;
        }

        static void PrintBoard(Checkers game)
        {
            Console.WriteLine("    0   1   2   3   4   5   6   7");
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{i} | ");
                for (int j = 0; j < 8; j++)
                {
                    Square square = game.board[i][j];
                    string token = square.HasFlag(Square.AIKing) ? "A*" :
                        square.HasFlag(Square.AIPiece) ? "A " :
                        square.HasFlag(Square.MeKing) ? "M*" :
                        square.HasFlag(Square.MePiece) ? "M " : ". ";
                    Console.Write(token + "| ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
