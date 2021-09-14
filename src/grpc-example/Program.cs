using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace grpc_example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // webBuilder.ConfigureKestrel(options =>
                    //     {
                    //         // Setup a HTTP/2 endpoint without TLS. To open grpc on http
                    //         options.ListenLocalhost(5000, o => o.Protocols = 
                    //             HttpProtocols.Http2);
                    //     });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
