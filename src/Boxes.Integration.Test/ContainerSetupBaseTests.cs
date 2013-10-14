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
namespace Boxes.Integration.Test
{
    using System;
    using System.Data;
    using System.Linq;
    using Boxes.Test;
    using Boxes.Test.Infrastructure;
    using Setup;
    using NUnit.Framework;
    using Setup.Registrations;

    public interface ILifeStyle {}

    public class Singleton : ILifeStyle {}

    public class Configure {}

    public class Register : RegisterBase<Type, Configure> 
    {
    }

    public class RegisterBaseTests : TestBase<IRegister<Type, Configure>>
    {
        [Test]
        public void Simple_setup()
        {
            Action(() =>
                {
                     var reg = new Register()
                        .LifeStyle(typeof(Singleton))
                        .Where(x => typeof(IDbCommand).IsAssignableFrom(x))
                        .AssociateWith(Contracts.SelfOnly);

                     return new Context<IRegister<Type, Configure>>(reg);
                });
            

        }

    }
}