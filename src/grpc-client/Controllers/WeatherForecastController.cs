using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Grpc.Net.Client;
using grpc_example.Protos;

namespace grpc_client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly HttpMessageHandler CLIENT_HANDLER = CreateHttpHandler();

        [HttpGet]
        public async Task<dynamic> Get()
        {
            using var channel = CreateGrpcChannel();
            
            var creditRequest = new CreditRequest {CustomerId = "id0201", Credit = 7000};

            var reply = await CallAndGetReply(channel, creditRequest);

            var message = new
            {
                message = $"Credit for customer {creditRequest.CustomerId} {(reply.IsAccepted ? "approved" : "rejected")}!"
            };

            return message;
        }

        private static GrpcChannel CreateGrpcChannel()
        {
            if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
            {
                return GrpcChannel.ForAddress("https://grpc-example:5001",
                    new GrpcChannelOptions {HttpHandler = CLIENT_HANDLER});

            }

            return GrpcChannel.ForAddress("https://grpc-example:6001");
        }

        private static async Task<CreditReply> CallAndGetReply(GrpcChannel channel, CreditRequest creditRequest)
        {
            var client = new CreditRatingCheck.CreditRatingCheckClient(channel);


            var reply = await client.CheckCreditRequestAsync(creditRequest);
            return reply;
        }

        private static HttpMessageHandler CreateHttpHandler()
        {
            // var certificate = new X509Certificate2("localpfx.pfx", "password");
            var certificate = new X509Certificate2("/root/.aspnet/https/grpc-example.com.crt");
            var certificateValidator = new SingleCertificateValidator(certificate);

            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = certificateValidator.Validate
            };
        }
    }
}
