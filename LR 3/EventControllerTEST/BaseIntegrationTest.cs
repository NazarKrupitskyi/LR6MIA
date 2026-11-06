using LR_3.Test;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventControllerTEST
{
    public class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        protected readonly HttpClient Client;
        protected readonly IServiceProvider _services;
        protected readonly JsonSerializerOptions JsonOptions;
        public BaseIntegrationTest(CustomWebApplicationFactory factory)
        {
            Client = factory.CreateClient();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
            _services = factory.Services;

            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
        }

        public async Task InitializeAsync()
        {
            using var scope = _services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
            await seeder.SeedTodosAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
