// Copyright 2012 - 2013 dbones.co.uk
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
namespace Boxes.Integration.Process
{
    using System.Collections.Generic;

    /// <summary>
    /// These nodes are used to implement a topological sort
    /// </summary>
    /// <typeparam name="T">the item associated with the node</typeparam>
    /// <remarks>
    /// based on http://www.patrickdewane.com/2009/03/topological-sort.html
    /// </remarks>
    public class Node<T>
    {
        /// <summary>
        /// The instance this node represents
        /// </summary>
        public T Element { get; private set; }
        
        /// <summary>
        /// Indicates if the node has been visited/processed
        /// </summary>
        public bool Visited { get;  set; }

        /// <summary>
        /// dependencies of this node
        /// </summary>
        public List<Node<T>> Dependencies { get; private set; }

        public Node(T element)
        {
            Element = element;
            Dependencies = new List<Node<T>>();
        }

        internal static void Visit(Node<T> n, ICollection<T> list)
        {
            //TODO: handle circular dependencies
            if (n.Visited)
            {
                return;
            }
            n.Visited = true;
            foreach (Node<T> dependency in n.Dependencies)
            {
                Visit(dependency, list);
            }
            list.Add(n.Element);
        }
    }
}
