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
namespace Boxes.Integration.Setup
{
    using System.Collections.Generic;
    using Boxes.Tasks;
    using Filters;
    using Registrations;

    /// <summary>
    /// abstract for the <see cref="IContainerSetup{TBuilder}"/>, inherit this in the IoC project
    /// </summary>
    /// <typeparam name="TBuilder">the builder which the IoC will use</typeparam>
    public abstract class ContainerSetupBase<TBuilder> : IContainerSetup<TBuilder>
    {
        private readonly IRegistrationTaskMapper<TBuilder> _registrationTaskMapper;
        private readonly List<IBoxesTask<RegistrationContext<TBuilder>>> _registraionTasks = new List<IBoxesTask<RegistrationContext<TBuilder>>>();
        private readonly Dictionary<string, ITypeRegistrationFilter> _packageTypeFilters;

        protected ContainerSetupBase(IRegistrationTaskMapper<TBuilder> registrationTaskMapper)
        {
            _registrationTaskMapper = registrationTaskMapper;
            _packageTypeFilters = new Dictionary<string, ITypeRegistrationFilter>();
            DefaultTypeRegistrationFilter = new DefaultTypeRegistrationFilter();
        }

        public virtual IEnumerable<IBoxesTask<RegistrationContext<TBuilder>>> Registrations { get { return _registraionTasks; } }

        public ITypeRegistrationFilter DefaultTypeRegistrationFilter { get; private set; }

        public ITypeRegistrationFilter GetTypeRegistrationFilter(string packageName)
        {
            ITypeRegistrationFilter filter;
            _packageTypeFilters.TryGetValue(packageName, out filter);
            return filter;
        }

        public void AddRegistration(IRegister registration)
        {
            var task = _registrationTaskMapper.CreateRegisterTask(registration.RegistrationMeta);
            _registraionTasks.Add(task);
        }

        public void SetDefaultFilter(ITypeRegistrationFilter typeRegistrationFilter)
        {
            DefaultTypeRegistrationFilter = typeRegistrationFilter;
        }

        public void AddPackgeLevelFilter(ITypeRegistrationFilter typeRegistrationFilter, params string[] packageNames)
        {
            foreach (var packageName in packageNames)
            {
                if (_packageTypeFilters.ContainsKey(packageName))
                {
                    _packageTypeFilters[packageName] = typeRegistrationFilter;
                }
                else
                {
                    _packageTypeFilters.Add(packageName, typeRegistrationFilter);
                }
            }
        }
    }
}