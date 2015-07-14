﻿// Copyright 2014-2015 Aaron Stannard, Petabridge LLC
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
  using System;
  using Serilog;
  using Serilog.Events;
  using Topshelf;

  public class Program
  {
    public static int Main(string[] args)
    {
      ConfigureLogging();

      return (int)HostFactory.Run(x =>
      {
        x.SetServiceName("Lighthouse");
        x.SetDisplayName("Lighthouse Service Discovery");
        x.SetDescription("Lighthouse Service Discovery for Akka.NET Clusters");

        x.UseAssemblyInfoForServiceInfo();
        x.RunAsLocalSystem();
        x.StartAutomatically();
        x.UseSerilog();
        x.Service<LighthouseService>();
        x.EnableServiceRecovery(r => r.RestartService(1));
      });
    }

    private static void ConfigureLogging()
    {
      var logLayout = "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}";

      Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithProcessId()
        .Enrich.WithThreadId()
        .Enrich.WithMachineName()
        .WriteTo.EventLog("Lighthouse", "Application", outputTemplate: logLayout, restrictedToMinimumLevel: LogEventLevel.Warning)
        .WriteTo.LiterateConsole(outputTemplate: logLayout, restrictedToMinimumLevel: LogEventLevel.Information)
        .CreateLogger();
    }
  }
}