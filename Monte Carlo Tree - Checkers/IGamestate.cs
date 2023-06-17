using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Carlo_Tree___Checkers
{
    public interface IGamestate
    {
        public int Value { get; set; }
        bool IsWin { get; }
        bool IsTie { get; }
        bool IsLoss { get; }
        bool IsTerminal { get; }
        IGamestate[] GetChildren();





    }
}
