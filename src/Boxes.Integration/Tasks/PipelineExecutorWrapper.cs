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
    using System.Collections.Generic;
    using System.Linq;
    using Boxes.Tasks;

    internal class PipelineExecutorWrapper<T>
    {
        private ICollection<IBoxesTask<T>> _tasks;
        private PipilineExecutor<T> _pipilineExecutor;

        private int _numberOfRegistrations =-1;

        public PipelineExecutorWrapper() { }

        public PipelineExecutorWrapper(IEnumerable<IBoxesTask<T>> tasks)
        {
            _tasks = tasks as ICollection<IBoxesTask<T>> ?? tasks.ToList();
        }

        public void UpdateTasksAsRequired(IEnumerable<IBoxesTask<T>> tasks)
        {
            _tasks = tasks as ICollection<IBoxesTask<T>> ?? tasks.ToList();
            UpdateTasksAsRequired();
        }

        public void UpdateTasksAsRequired()
        {
            var currentNumberOfRegistrations = _tasks.Count();

            if (currentNumberOfRegistrations == _numberOfRegistrations)
            {
                return;
            }
            _numberOfRegistrations = currentNumberOfRegistrations;
            _pipilineExecutor = _tasks.CreatePipeline();
        }

        public IEnumerable<T> Execute(IEnumerable<T> item)
        {
            return _pipilineExecutor.Execute(item);
        }
    }
}