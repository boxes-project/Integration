namespace Temp
{
    using System.Collections.Generic;
    using System.Linq;
    using Boxes.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PipelineExecutorWrapper<T>
    {
        private ICollection<IBoxesTask<T>> _tasks;
        private PipilineExecutor<T> _pipilineExecutor;

        private int _numberOfRegistrations = -1;

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