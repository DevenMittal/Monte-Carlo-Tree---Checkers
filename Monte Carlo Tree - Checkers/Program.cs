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

            Play game = new Play(root);
            while (game.IsGameOver() == State.StillPlaying)
            {
                int xAxis = 0;
                Console.Clear();
                Console.WriteLine("____0____1____2____3____4____5____6____7____");


                for (int i = 0; i < 8; i++)
                {

                    Console.Write($"{xAxis} |");
                    xAxis++;
                    for (int j = 0; j < 8; j++)
                    {
                        Square num = game.rootTemp.board[i][j];
                        if (num.HasFlag(Square.AIPiece))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" () ");
                        }
                        else if (num.HasFlag(Square.AIPiece))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" [] ");
                        }
                        else if (num.HasFlag(Square.MePiece))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" () ");
                        }
                        else if (num.HasFlag(Square.MeKing))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" [] ");
                        }
                        else
                        {
                            Console.Write("    ");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|");

                    }
                    Console.WriteLine();

                    for (int k = 0; k < 43; k++)
                    {
                        Console.Write("_");
                    }
                    Console.WriteLine();


                }
                int currentX = int.Parse(Console.ReadLine());
                int currentY = int.Parse(Console.ReadLine());
                //Console.WriteLine(GameMaster.count);
                int x = int.Parse(Console.ReadLine());
                //Console.WriteLine();
                int y = int.Parse(Console.ReadLine());
                //something
                

                game.Move((currentX, currentY), (x, y));


            }
        }
    }
}