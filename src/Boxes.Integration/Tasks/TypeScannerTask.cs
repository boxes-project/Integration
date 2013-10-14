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
namespace Boxes.Integration.Tasks
{
    using System;
    using System.Collections.Generic;
    using Boxes.Tasks;

    /// <summary>
    /// Generic task which may be helpful
    /// </summary>
    public class TypeScannerTask : IBoxesTask<Type>
    {
        private readonly Func<Type, bool> _filter;
        private readonly List<Type> _results = new List<Type>();

        public TypeScannerTask(Func<Type, bool> filter)
        {
            _filter = filter;
        }

        public IEnumerable<Type> Results { get { return _results; } }

        public bool CanHandle(Type item)
        {
            return _filter(item);
        }

        public void Execute(Type item)
        {
            _results.Add(item);
        }
    }
}