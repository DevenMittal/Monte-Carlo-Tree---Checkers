using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Carlo_Tree___Checkers
{
    internal class GameMaster<T> where T : IGamestate
    {
        public static T MCTS(int iterations, T startingState, Random random)
        {
            //Generate the monte-carlo tree
            var rootNode = new Node<T>(startingState);
            for (int i = 0; i < iterations; i++)
            {
                var selectedNode = Select(rootNode);
                var expandedChild = Expand(selectedNode);
                int value = Simulate(expandedChild, random);
                Backpropagate(value, expandedChild);
            }

            //return the best child
            var sortedChildren = rootNode.Children.OrderByDescending((state) => state.W);
            var topChild = sortedChildren.First();
            return topChild.State;
        }

        static Node<T> Select(Node<T> rootNode)
        {
            Node<T> currentNode = rootNode;
            while (currentNode.IsExpanded)
            {
                //Find the index of the child with the highest UCT value
                Node<T> highestUCTChild = null;
                double highestUCT = double.NegativeInfinity;
                foreach (var child in currentNode.Children)
                {
                    double val = child.UCT();
                    if (val > highestUCT)
                    {
                        highestUCT = val;
                        highestUCTChild = child;
                    }
                }
                if (highestUCTChild == null) break;
                //Set the current node to the child with the highest UCT value
                currentNode = highestUCTChild;
            }
            //Return the selected node
            return currentNode;
        }
        static Node<T> Expand(Node<T> currentNode)
        {
            currentNode.GenerateChildren();

            if (currentNode.Children.Length == 0) return currentNode;
            return currentNode.Children[0];
        }
        static int Simulate(Node<T> currentNode, Random random)
        {
            while (!currentNode.State.IsTerminal)
            {
                currentNode.GenerateChildren();
                int randomIndex = random.Next(0, currentNode.Children.Length);
                currentNode = currentNode.Children[randomIndex];
            }
            if (currentNode.State.IsWin) return 1;
            else if (currentNode.State.IsTie) return 0;
            else return -1;
        }

        static void Backpropagate(int value, Node<T> simulatedNode)
        {
            Node<T> currentNode = simulatedNode;
            while (currentNode != null)
            {
                value = -value;
                currentNode.N++;
                currentNode.W += value;
                currentNode = currentNode.Parent;
            }
        }

    }
}
