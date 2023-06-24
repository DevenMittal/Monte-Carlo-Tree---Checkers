using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Carlo_Tree___Checkers
{
    internal class Node<T> where T : IGamestate
    {
        public double W { get; set; }
        public double N { get; set; }
        public Node<T> Parent { get; set; } 

        public Node<T>[] Children { get; set; }

        public T State { get; set; }

        public bool IsExpanded { get; set; }

        public Node(T startingState)
        {
            State = startingState;
        }
        
        public Node<T>[] GenerateChildren()
        {
            if (Children != null) return Children; 
            IGamestate[] childrenStates = State.GetChildren();
            Node<T>[] childNodes = new Node<T>[childrenStates.Length];
            for (int i = 0; i < childNodes.Length; i++)
            {
                childNodes[i] = new Node<T>((T)childrenStates[i]);
            }
            Children= childNodes;
            return Children;
        }

        public double UCT()
        {
            return W / N + 1.5 * Math.Sqrt(Math.Log(Parent.N) / N);
        }

    }
}
