using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using grpc_example.Protos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace grpc_client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                    // webBuilder.ConfigureKestrel(options =>
                    // {
                    //     // Setup a HTTP/2 endpoint without TLS.
                    //     options.ListenLocalhost(5000, o => o.Protocols = 
                    //         HttpProtocols.Http2);
                    // });
                    // }
                    webBuilder.UseStartup<Startup>();
                });
    }
}
