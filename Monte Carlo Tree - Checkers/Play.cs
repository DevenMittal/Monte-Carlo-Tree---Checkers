using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Carlo_Tree___Checkers
{
    enum State
    {
        AIWin,
        IWin,
        Tie,
        StillPlaying
    }


    internal class Play : GameMaster<Checkers> 
    {

        public Checkers rootTemp;

        public Play(Checkers root)
        {
            rootTemp = root;
        }

        public State IsGameOver()
        {
            if (rootTemp.IsWin == true) return State.IWin;
            if (rootTemp.IsLoss == true) return State.AIWin;
            if (rootTemp.IsTie == true) return State.Tie;
            return State.StillPlaying;
        }
        public bool Move((int x, int y)piece, (int x, int y) move)
        {
            if (move.x < 8 && move.x >= 0 && move.y < 8 && move.y >= 0)
            {
                if (Math.Abs(move.x - piece.x) == 1 && Math.Abs(move.y - piece.y) == 1 && move.x < piece.x)
                {
                    rootTemp.board[move.x][move.y] = Square.MePiece;
                    rootTemp.board[piece.x][piece.y] = Square.Empty;
                }
                else if (Math.Abs(move.x - piece.x) == 2 && Math.Abs(move.y - piece.y) == 2 && move.x < piece.x)
                {
                    if (move.y > piece.y && rootTemp.board[piece.x - 1][piece.y + 1] == Square.AI)
                    {
                        rootTemp.board[move.x][move.y] = Square.MePiece;
                        rootTemp.board[piece.x][piece.y] = Square.Empty;
                        rootTemp.board[piece.x - 1][piece.y + 1] = Square.Empty;
                    }
                }
                else return false;
            }
            else return false;

            if (rootTemp.IsTerminal == true && IsGameOver() != State.StillPlaying)
            {
                return false;
            }

            rootTemp.Player = Square.AI;
            rootTemp = MCTS(1600, rootTemp, new Random());

            return true;
        }

    }

}
