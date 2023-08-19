using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Carlo_Tree___Checkers
{
    internal class Node<T> where T : IGamestate<T>
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
            IsExpanded = true;
            if (Children != null) return Children; 
            T[] childrenStates = State.GetChildren();
            Node<T>[] childNodes = new Node<T>[childrenStates.Length];
            for (int i = 0; i < childNodes.Length; i++)
            {
                childNodes[i] = new Node<T>(childrenStates[i]);
                childNodes[i].Parent = this;
            }
            Children = childNodes;
            return Children;
        }

        public double UCT()
        {
           // if (N == 0) return double.PositiveInfinity;
            return W / N + 1.5 * Math.Sqrt(Math.Log(Parent.N, Math.E) / N);
        }

    }
}
