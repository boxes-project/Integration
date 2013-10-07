// Copyright 2012 - 2013 dbones.co.uk (David Rundle)
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
    using System.Linq;

    /// <summary>
    /// Process the packages using a simple Topological order
    /// </summary>
    public class TopologicalProcessOrder : IProcessOrder
    {
        private readonly IDictionary<Package, Node<Package>> _packagesToNodes = new Dictionary<Package, Node<Package>>(); 
        private readonly IDictionary<Module, Package> _modulesToPackages = new Dictionary<Module, Package>(); 

        public IEnumerable<Package> Arrange(IEnumerable<Package> packages)
        {
            //as the packages can expose more than
            //one module, we run a 2 phase (pain)
            var newPackages = packages as List<Package> ?? packages.ToList();
            foreach (var package in newPackages.Where(x => !_packagesToNodes.ContainsKey(x)))
            {
                var node = new Node<Package>(package);
                foreach (var export in package.Manifest.Exports)
                {
                    _modulesToPackages.Add(export, package);
                }
                _packagesToNodes.Add(package, node);
            }

            var nodes = newPackages.Select(SetupNode);
            return nodes.PerformTopologicalSort();
        }

        private Node<Package> SetupNode(Package package)
        {
            var node = _packagesToNodes[package];

            if (node.Dependencies.Any())
            {
                return node;
            }

            HashSet<Package> uniquePackages = new HashSet<Package>();
            foreach (var import in package.Manifest.Imports)
            {
                var dependency = _modulesToPackages[import];
                uniquePackages.Add(dependency);
            }
            foreach (var uniquePackage in uniquePackages)
            {
                var dependencyNode = _packagesToNodes[uniquePackage];
                node.Dependencies.Add(dependencyNode);
            }
            return node;
        }
    }
}