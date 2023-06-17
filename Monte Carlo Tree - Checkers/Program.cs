namespace Monte_Carlo_Tree___Checkers
{
    enum Square
    {
        empty = 0,
        AI = -1,
        AIKing = -2,
        Me = 1,
        MeKing = 2

    }
    internal class Program
    {
        
        static void Main(string[] args)
        {

            Square[][] inputs = new Square[8][];
            inputs[0] = new Square[] { Square.empty, Square.AI, Square.empty, Square.AI, Square.empty, Square.AI, Square.empty, Square.AI };
            inputs[1] = new Square[] { Square.AI, Square.empty, Square.AI, Square.empty, Square.AI, Square.empty, Square.AI, Square.empty };
            inputs[2] = new Square[] { Square.empty, Square.AI, Square.empty, Square.AI, Square.empty, Square.AI, Square.empty, Square.AI };
            inputs[3] = new Square[] { Square.empty, Square.empty, Square.empty, Square.empty, Square.empty, Square.empty, Square.empty, Square.empty };
            inputs[4] = new Square[] { Square.empty, Square.empty, Square.empty, Square.empty, Square.empty, Square.empty, Square.empty, Square.empty };
            inputs[5] = new Square[] { Square.Me, Square.empty, Square.Me, Square.empty, Square.Me, Square.empty, Square.Me, Square.empty };
            inputs[6] = new Square[] { Square.empty, Square.Me, Square.empty, Square.Me, Square.empty, Square.Me, Square.empty, Square.Me };
            inputs[7] = new Square[] { Square.Me, Square.empty, Square.Me, Square.empty, Square.Me, Square.empty, Square.Me, Square.empty };


            Checkers root = new Checkers(inputs, 1);

            Play game = new Play(root);

            while (game.IsGameOver() == State.StillPlaying)
            {

                Console.Clear();

                for (int i = 0; i < 8; i++)
                {

                    for (int j = 0; j < 8; j++)
                    {
                        Square num = game.rootTemp.board[i][j];
                        if (num == Square.AI)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" () ");
                        }
                        else if (num == Square.Me)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" () ");
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|");

                    }
                    Console.WriteLine();

                    for (int k = 0; k < 32; k++)
                    {
                        Console.Write("_");
                    }
                    Console.WriteLine();


                }
                //Console.WriteLine(GameMaster.count);
                int x = int.Parse(Console.ReadLine());
                //Console.WriteLine();
                int y = int.Parse(Console.ReadLine());
                //something
                game.Move((x, y), (x, y));


            }
        }
    }
}