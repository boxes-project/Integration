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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// helpful extensions
    /// </summary>
    public static class TopologicalSortExtensions
    {
        /// <summary>
        /// perform a topological sort
        /// </summary>
        /// <typeparam name="T">the type the nodes will represent</typeparam>
        /// <param name="nodes">the list of nodes to sort</param>
        /// <returns>sorted by dependency</returns>
        public static IEnumerable<T> PerformTopologicalSort<T>(this IEnumerable<Node<T>> nodes)
        {
            var list = new List<T>();
            var enumerable = nodes as List<Node<T>> ?? nodes.ToList();
            enumerable.ForEach(n => n.Visited = false);
            enumerable.ForEach(n => Node<T>.Visit(n, list));
            return list;
        }

        /// <summary>
        /// for each
        /// </summary>
        /// <typeparam name="T">the type in the collection</typeparam>
        /// <param name="collection">the collection</param>
        /// <param name="action">action to perform on each item</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}