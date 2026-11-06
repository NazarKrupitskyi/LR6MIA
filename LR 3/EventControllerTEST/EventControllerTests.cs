using LR_3.DBCommunication.Documents;
using LR_3.Models;
using LR_3.Test;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EventControllerTEST
{
    namespace EventControllerTEST
    {
        public class EventControllertests : BaseIntegrationTest
        {
            public EventControllertests(CustomWebApplicationFactory factory) : base(factory) { }

            // ------------------------------------------------------------
            // GET: /api/event
            // ------------------------------------------------------------
            [Fact]
            public async Task GetEvents_ReturnsSeededItems()
            {
                var response = await Client.GetAsync("/api/event");
                response.EnsureSuccessStatusCode();

                var items = await response.Content.ReadFromJsonAsync<List<EventDocument>>(JsonOptions);

                Assert.NotNull(items);
                Assert.Equal(3, items.Count);
            }

            // ------------------------------------------------------------
            // GET: /api/event/{id}
            // ------------------------------------------------------------
            [Fact]
            public async Task GetEventById_ReturnsCorrectItem()
            {
                var list = await Client.GetFromJsonAsync<List<EventDocument>>("/api/event", JsonOptions);
                Assert.NotNull(list);
                var firstId = list[0].Id;

                var response = await Client.GetAsync($"/api/event/{firstId}");
                response.EnsureSuccessStatusCode();

                // ДОДАНО JsonOptions
                var item = await response.Content.ReadFromJsonAsync<EventDocument>(JsonOptions);

                Assert.NotNull(item);
                Assert.Equal(firstId, item.Id);
            }

            [Fact]
            public async Task GetEventById_ReturnsNotFound_ForNonExistingId()
            {
                var response = await Client.GetAsync("/api/event/6748a68293acd000228f0000");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }

            // ------------------------------------------------------------
            // POST: /api/event
            // ------------------------------------------------------------
            [Fact]
            public async Task CreateEvent_ReturnsCreatedItem()
            {
                var newEvent = new EventDocument
                {
                    Title = "Integration Test Create",
                    Description = "Testing POST method",
                    location = "Online",
                    startDate = DateTime.UtcNow.AddDays(10),
                    status = EventStatus.Published
                };

                var response = await Client.PostAsJsonAsync("/api/event", newEvent, JsonOptions);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var created = await response.Content.ReadFromJsonAsync<EventDocument>(JsonOptions);
                Assert.NotNull(created);
                Assert.Equal("Integration Test Create", created.Title);

                var list = await Client.GetFromJsonAsync<List<EventDocument>>("/api/event", JsonOptions);
                Assert.NotNull(list);
                Assert.Equal(4, list.Count);
            }

            // ------------------------------------------------------------
            // PUT: /api/event/{id}
            // ------------------------------------------------------------
            [Fact]
            public async Task UpdateEvent_ReturnsNoContent_AndUpdatesInDatabase()
            {
                var list = await Client.GetFromJsonAsync<List<EventDocument>>("/api/event", JsonOptions);
                Assert.NotNull(list);
                var item = list.First();

                var updated = new EventDocument
                {
                    Id = item.Id,
                    Title = "Updated Title",
                    Description = "Updated Description",
                    location = item.location,
                    startDate = item.startDate,
                    status = item.status
                };

                var response = await Client.PutAsJsonAsync($"/api/event/{item.Id}", updated, JsonOptions);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                var updatedFromDb = await Client.GetFromJsonAsync<EventDocument>($"/api/event/{item.Id}", JsonOptions);
                Assert.NotNull(updatedFromDb);
                Assert.Equal("Updated Title", updatedFromDb.Title);
            }

            [Fact]
            public async Task UpdateEvent_ReturnsNotFound_ForNonExistingId()
            {
                var nonExistingId = "6748a68293acd000228f0000";

                var updated = new EventDocument
                {
                    Id = nonExistingId, 
                    Title = "Valid Title",
                    Description = "Valid Description",
                    location = "Valid Location",
                    startDate = DateTime.UtcNow,
                    status = EventStatus.Published 
                };

                var response = await Client.PutAsJsonAsync($"/api/event/{nonExistingId}", updated, JsonOptions);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }

            // ------------------------------------------------------------
            // DELETE: /api/event/{id}
            // ------------------------------------------------------------
            [Fact]
            public async Task DeleteEvent_RemovesItemFromDatabase()
            {
                var list = await Client.GetFromJsonAsync<List<EventDocument>>("/api/event", JsonOptions);
                Assert.NotNull(list);
                var item = list.First();

                var response = await Client.DeleteAsync($"/api/event/{item.Id}");
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                var allAfter = await Client.GetFromJsonAsync<List<EventDocument>>("/api/event", JsonOptions);
                Assert.NotNull(allAfter);
                Assert.Equal(2, allAfter.Count);
            }

            [Fact]
            public async Task DeleteEvent_ReturnsNotFound_ForNonExistingId()
            {
                var response = await Client.DeleteAsync("/api/event/6748a68293acd000228f0000");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
