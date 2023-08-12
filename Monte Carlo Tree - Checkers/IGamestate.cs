using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Carlo_Tree___Checkers
{
    public interface IGamestate<TSelf> where TSelf : IGamestate<TSelf>
    {
        public int Value { get; set; }
        bool IsWin { get; }
        bool IsTie { get; }
        bool IsLoss { get; }
        bool IsTerminal { get; }

        void Reset();

        public bool IsEquivelent(TSelf other);

        TSelf[] GetChildren();





    }
}
