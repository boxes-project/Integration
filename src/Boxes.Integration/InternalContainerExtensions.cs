// Copyright 2012 - 2013 dbones.co.uk & Boxes Contrib Team
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
namespace Boxes.Integration
{
    using InternalIoc;

    /// <summary>
    /// some extensions for the internal container
    /// </summary>
    public static class InternalContainerExtensions
    {
        public static void Add<TContract, TService>(this IInternalContainer internalContainer) where TService : TContract
        {
            internalContainer.Add(typeof(TContract), typeof(TService));
        }

        public static TContract Resolve<TContract>(this IInternalContainer internalContainer)
        {
            return (TContract)internalContainer.Resolve(typeof(TContract));
        }
    }
}