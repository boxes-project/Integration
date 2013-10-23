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

    /// <summary>
    /// wrappers arround a single pipeline, and offers a simple
    /// way to update the tasks the pipeline will carry out.
    /// </summary>
    /// <typeparam name="T">the type which the tasks in the pipeline are</typeparam>
    public class PipelineExecutorWrapper<T>
    {
        private ICollection<IBoxesTask<T>> _tasks;
        private PipilineExecutor<T> _pipilineExecutor;

        private int _numberOfRegistrations =-1;

        /// <summary>
        /// create wrapper with no tasks
        /// </summary>
        public PipelineExecutorWrapper()
        {
        }

        /// <summary>
        /// create wrapper, and add a number of tasks ready for the pipeline
        /// </summary>
        /// <param name="tasks">the tasks for the underlying pipeline</param>
        public PipelineExecutorWrapper(IEnumerable<IBoxesTask<T>> tasks) : this()
        {
            _tasks = tasks as ICollection<IBoxesTask<T>> ?? tasks.ToList();
            UpdateTasksAsRequired();
        }

        /// <summary>
        /// update the tasks in the pipeline
        /// </summary>
        /// <param name="tasks">sets the tasks for the pipeline</param>
        public void UpdateTasksAsRequired(IEnumerable<IBoxesTask<T>> tasks)
        {
            _tasks = tasks as ICollection<IBoxesTask<T>> ?? tasks.ToList();
            UpdateTasksAsRequired();
        }

        /// <summary>
        /// update the tasks in the pipeline
        /// </summary>
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

        /// <summary>
        /// execute the pipeline
        /// </summary>
        /// <param name="item">items to process</param>
        /// <returns>the collection of items which was passed in</returns>
        public IEnumerable<T> Execute(IEnumerable<T> item)
        {
            return _pipilineExecutor.Execute(item);
        }
    }
}