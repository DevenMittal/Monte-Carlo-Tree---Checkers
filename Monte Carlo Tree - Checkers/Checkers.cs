using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Carlo_Tree___Checkers
{
    internal class Checkers: IGamestate
    {

        public int Value { get; set; }
        public bool IsTie
        {
            get
            {
                if (WinnerCheck() == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsLoss
        {
            get
            {
                if (WinnerCheck() == -1)
                {
                    return true;
                }
                return false;
            }
        }
        public bool IsWin
        {
            get
            {
                if (WinnerCheck() == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsTerminal
        {
            get
            {
                return (IsWin || IsTie || IsLoss) && GetChildren().Count() == 0;
            }
        }

        public Square[][] board;
        public int Player;

        public Checkers(Square[][] board, int player)
        {
            this.board = board;
            Player = player;
        }



        int WinnerCheck()
        {
            bool foundAI = false;
            bool foundMe = false;
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[0].Length; j++)
                {
                    if (board[i][j] == Square.AI)
                    {
                        foundAI = true;
                    }
                    if (board[i][j] == Square.Me)
                    {
                        foundMe = true;
                    }
                }        
            }
            if (!foundAI) return 1;
            if (!foundMe) return -1;
            if (GetChildren() == null)
            {
                if (Player == 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            return 2;
        }
        public Square[][] MakeAMove(int XMove, int YMove, int XEnemySpot = 10, int YEnemySpot = 10)
        {
            Square[][] boardClone = new Square[8][];

            for (int k = 0; k < 8; k++)
            {
                boardClone[k] = new Square[8];
                for (int z = 0; z < 8; z++)
                {

                    boardClone[k][z] = board[k][z];
                }
            }
            boardClone[XMove][YMove] = Square.AI;

            if (XEnemySpot != 10)
            {
                boardClone[XEnemySpot][YEnemySpot] = Square.empty;   
            }
            return boardClone;
        }
        public IGamestate[] GetChildren()
        {
            List<IGamestate> children = new List<IGamestate>(0);

            
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {   
                    //Player is AI
                    if (Player == 0)
                    {
                        if (board[i][j] == Square.AI)
                        {
                            

                            //bottom left of AI
                            if (board[i+1][j-1] == Square.empty)
                            {
                                children.Add(new Checkers(MakeAMove(i+1, j-1), Math.Abs(Player - 1)));
                            }
                            //bottom left of AI With Enemy
                            if (board[i + 1][j - 1] == Square.Me && board[i+2][j-2] == Square.empty)
                            {
                                children.Add(new Checkers(MakeAMove(i+2, j-2, i+1, j-1), Math.Abs(Player - 1)));
                            }
                            //bottom right of AI
                            if (board[i + 1][j + 1] == Square.empty)
                            {
                                children.Add(new Checkers(MakeAMove(i + 1, j + 1), Math.Abs(Player - 1)));
                            }
                            //bottom right of AI With Enemy
                            if (board[i + 1][j + 1] == Square.Me && board[i + 2][j + 2] == Square.empty)
                            {
                                children.Add(new Checkers(MakeAMove(i + 2, j + 2, i + 1, j + 1), Math.Abs(Player - 1)));
                            }
                        }                       
                    }
                    if (Player == 1)
                    {
                        if (board[i][j] == Square.Me)
                        {
                            //top left of AI
                            if (board[i - 1][j - 1] == Square.empty)
                            {
                                children.Add(new Checkers(MakeAMove(i - 1, j - 1), Math.Abs(Player - 1)));
                            }
                            //bottom left of AI With Enemy
                            if (board[i - 1][j - 1] == Square.Me && board[i - 2][j - 2] == Square.empty)
                            {
                                children.Add(new Checkers(MakeAMove(i + 2, j - 2, i + 1, j - 1), Math.Abs(Player - 1)));
                            }
                            //bottom right of AI
                            if (board[i - 1][j + 1] == Square.empty)
                            {
                                children.Add(new Checkers(MakeAMove(i - 1, j + 1), Math.Abs(Player - 1)));
                            }
                            //bottom right of AI With Enemy
                            if (board[i - 1][j + 1] == Square.Me && board[i - 2][j + 2] == Square.empty)
                            {
                                children.Add(new Checkers(MakeAMove(i -2, j + 2, i - 1, j + 1), Math.Abs(Player - 1)));
                            }
                        }
                    }
                }
            }
            

            
            return children.ToArray();
        }
    }
}
