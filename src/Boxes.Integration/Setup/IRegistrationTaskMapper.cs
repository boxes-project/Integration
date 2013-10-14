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
namespace Boxes.Integration.Setup
{
    using Boxes.Tasks;

    /// <summary>
    /// this provides a mechanism to create the required boxes tasks for registration
    /// </summary>
    /// <typeparam name="TBuilder">the IoC builder</typeparam>
    public interface IRegistrationTaskMapper<TBuilder>
    {

        /// <summary>
        /// create a boxes registration task from the <see cref="RegistrationMeta"/>
        /// </summary>
        /// <param name="registration">the registration to be tasked up</param>
        IBoxesTask<RegistrationContext<TBuilder>> CreateRegisterTask(RegistrationMeta registration);
    }
}