// Copyright 2014-2015 Aaron Stannard, Petabridge LLC
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

namespace Lighthouse
{
    using System.Threading.Tasks;
    using Akka.Actor;

    public class LighthouseService
    {
        private readonly string ipAddress;
        private readonly int? port;

        private ActorSystem lighthouseSystem;

        public LighthouseService() : this(null, null)
        {
        }

        public LighthouseService(string ipAddress, int? port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public void Start()
        {
            this.lighthouseSystem = LighthouseHostFactory.LaunchLighthouse(this.ipAddress, this.port);
        }

        public async Task StopAsync()
        {
            await this.lighthouseSystem.Terminate();
        }
    }
}