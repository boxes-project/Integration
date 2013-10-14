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
namespace Boxes.Integration.Trust
{
    using Contexts;
    using Filters;

    /// <summary>
    /// a trust manager is responsible for validating components to see if they are to be trusted
    /// some things which the application may be interested in is if a dll has been tampered with
    /// or if a class type is being exposed from a known dll or a dll which has a certain public key
    /// </summary>
    /// <remarks>
    /// This works on a optimistic way, it looks for a black listing, not a white one.
    /// </remarks>
    public interface ITrustManager
    {
        /// <summary>
        /// this will detail if a class/dll etc is trusted
        /// </summary>
        /// <param name="context">the current context to investigate</param>
        /// <returns>return true if the context can be trusted</returns>
        void IsTrusted(TrustContext context);

        /// <summary>
        /// add a trust filter for the manager to use
        /// </summary>
        /// <param name="trust">the filer to be applied</param>
        void AddTrust(ITrustFilter trust);
    }
}