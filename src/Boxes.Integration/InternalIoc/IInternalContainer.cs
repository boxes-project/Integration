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
namespace Boxes.Integration.InternalIoc
{
    using System;

    /// <summary>
    /// A simple internal container, which will manage the services available in Boxes.
    /// </summary>
    public interface IInternalContainer : IDisposable
    {
        void Add(Type contract, Type service);
        
        void setInstance(Type type, object instance);

        object Resolve(Type contract);
    }
}