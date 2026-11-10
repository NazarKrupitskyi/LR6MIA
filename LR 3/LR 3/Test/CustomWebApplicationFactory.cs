//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.Extensions.DependencyInjection;
//using Mongo2Go;
//using MongoDB.Driver;
//using System.Text.Json.Serialization;

//namespace LR_3.Test
//{
//        public class CustomWebApplicationFactory : WebApplicationFactory<Program>
//        {
//            private MongoDbRunner? _runner;

//            protected override void ConfigureWebHost(IWebHostBuilder builder)
//            {
//                builder.ConfigureServices(services =>
//                {
//                    var runner = MongoDbRunner.Start();
//                    _runner = runner;

//                    var descriptor = services.SingleOrDefault(s =>
//                        s.ServiceType == typeof(IMongoClient));

//                    if (descriptor != null)
//                        services.Remove(descriptor);

//                    services.AddSingleton<IMongoClient>(_ =>
//                        new MongoClient(runner.ConnectionString));

//                    services.AddSingleton<IMongoDatabase>(sp =>
//                    {
//                        var client = sp.GetRequiredService<IMongoClient>();
//                        return client.GetDatabase("IntegrationTestsDb");
//                    });

//                    services.AddSingleton<DatabaseSeeder>();

//                    services.AddAuthentication(options =>
//                    {
//                        options.DefaultAuthenticateScheme = "Test";
//                        options.DefaultChallengeScheme = "Test";
//                    })
//                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

             

//                });
//            }

//            protected override void Dispose(bool disposing)
//            {
//                base.Dispose(disposing);
//                _runner?.Dispose();
//            }
//        }
//    }
