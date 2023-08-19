using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Carlo_Tree___Checkers
{
    internal class Checkers : IGamestate<Checkers>
    {
        public List<Checkers> children;
        public int Value { get; set; }
        public bool IsTie => WinnerCheck() == 0;

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
                return WinnerCheck() != 2 && GetChildren().Length == 0;
            }
        }

        public Square[][] board;
        public Square Player;

        public Checkers(Square[][] board, Square player)
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
                    if (board[i][j].HasFlag(Square.AI))
                    {
                        foundAI = true;
                    }
                    else if (board[i][j].HasFlag(Square.MePiece))
                    {
                        foundMe = true;
                    }
                }
            }
            if (!foundAI) return 1;
            if (!foundMe) return -1;
            if (GetChildren().Length == 0)
            {
                if (Player == 0) return 1;

                else return -1;

            }
            return 2;
        }
        public Square[][] MakeAMove(int XPos, int YPos, int XMove, int YMove, int XEnemySpot = 10, int YEnemySpot = 10)
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
            boardClone[XMove][YMove] = board[XPos][YPos];
            boardClone[XPos][YPos] = Square.Empty;
            if (XEnemySpot != 10)
            {
                boardClone[XEnemySpot][YEnemySpot] = Square.Empty;
            }
            if (XMove == 0)
            {
                if (boardClone[XMove][YMove] == Square.MePiece)
                {
                    boardClone[XMove][YMove] = Square.MeKing;
                }
            }
            if (XMove == 7)
            {
                if (boardClone[XMove][YMove] == Square.AIPiece)
                {
                    boardClone[XMove][YMove] = Square.AIKing;
                }
            }
            return boardClone;
        }
        public bool IsEquivelent(Checkers State)
        {
            for (int j = 0; j < State.board.Length; j++)
            {
                for (int k = 0; k < State.board[j].Length; k++)
                {
                    if (board[j][k] != State.board[j][k])
                        return false;
                }
            }
            return true;
        }
        public Checkers[] GetChildren()
        {
            if (children == null)
            {
                children = new List<Checkers>(0);

                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board[i].Length; j++)
                    {
                        if ((board[i][j] & Square.AI) != Player) continue;

                        void TryMakeMove(Point point)
                        {
                            int MoveX = i + point.X;
                            int MoveY = j + point.Y;
                            int EnemyX = i + 2 * point.X;
                            int EnemyY = j + 2 * point.Y;

                            if (WithinBoard(MoveX, MoveY))
                            {
                                if (board[MoveX][MoveY] == Square.Empty)
                                {
                                    children.Add(new Checkers(MakeAMove(i, j, MoveX, MoveY), Player ^ Square.AI));
                                    if (i == 1 && j == 1)
                                    {
                                        ;
                                    }
                                }
                                else if (WithinBoard(EnemyX, EnemyY) && (board[MoveX][MoveY] & Square.AI) != Player && board[EnemyX][EnemyY] == Square.Empty)
                                {
                                    children.Add(new Checkers(MakeAMove(i, j, EnemyX, EnemyY, MoveX, MoveY), Player ^ Square.AI));
                                    if (i == 1 && j == 1)
                                    {
                                        ;
                                    }
                                }


                            }
                        }
                        if (board[i][j].HasFlag(Square.Up))
                        {
                            TryMakeMove(new(-1, -1));
                            TryMakeMove(new(-1, 1));
                        }
                        if (board[i][j].HasFlag(Square.Down))
                        {
                            TryMakeMove(new(1, 1));
                            TryMakeMove(new(1, -1));
                        }
                    }
                }
            }
            //else
            //{
            //    var kids = children;
            //    children = null;
            //    var goodKids = GetChildren();
            //    for (int i = 0; i < kids.Count; i++)
            //    {
            //        var localKid = kids[i];
            //        var localChild = children[i];

            //        for (int j = 0; j < localKid.board.Length; j++)
            //        {
            //            for (int k = 0; k < localKid.board[j].Length; k++)
            //            {
            //                if (localKid.board[j][k] != localChild.board[j][k])
            //                    return goodKids;
            //            }
            //        }
            //    }
            //}
            return children.ToArray();
        }
        public bool WithinBoard(int x, int y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7) return false;
            return true;
        }

        public void Reset()
        {
            children = null;
        }
    }
}
