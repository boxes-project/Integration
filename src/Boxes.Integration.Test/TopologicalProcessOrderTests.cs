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
    using Boxes.Test;
    using Boxes.Test.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Process;
    using NUnit.Framework;

    public class TopologicalProcessOrderTests : TestBase<TopologicalProcessOrder>
    {
        [Test]
        public void Order_dependencies()
        {
            Arrange(() =>
                {
                    Func<IEnumerable<string>, IEnumerable<Module>> createModules =
                        titles => titles.Select(title => new Module(title, new Version(1, 0)));

                    Func<string, IEnumerable<string>, IEnumerable<string>, Package> createPackage =
                        (title, exports, imports) =>
                            {
                                var manifest = new Manifest(title, new Version(1, 0), "title", createModules(exports),
                                                            createModules(imports));
                                var package = new Package(manifest, title);
                                return package;
                            };

                    dynamic ctx = new Context<TopologicalProcessOrder>(new TopologicalProcessOrder());
                    ctx.PackagesToSort = new List<Package>()
                        {
                            createPackage("package1", new[] {"a1"}, new string[] {}),
                            createPackage("package3", new[] {"a3"}, new[] {"a1", "a2"}),
                            createPackage("package2", new[] {"a2"}, new[] {"a1"}),
                            createPackage("package4", new[] {"a4"}, new[] {"a3"}),
                        };
                    return ctx;
                });
            Action(ctx => ((dynamic) ctx).Result = ctx.Sut.Arrange(((dynamic) ctx).PackagesToSort));
            Assert(ctx => ((dynamic) ctx).Result[0].Name == "package1");
            Assert(ctx => ((dynamic) ctx).Result[1].Name == "package2");
            Assert(ctx => ((dynamic) ctx).Result[2].Name == "package3");
            Assert(ctx => ((dynamic) ctx).Result[3].Name == "package4");
            Execute();
        }
    }
}
