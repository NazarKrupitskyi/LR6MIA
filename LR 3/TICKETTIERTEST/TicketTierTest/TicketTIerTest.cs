using AutoMapper;
using LR_3.DBCommunication.Repositories.Interfaces;
using LR_3.Models;
using LR_3.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTierTest
{

    public class TicketTIerTest
    {
        private readonly Mock<ITicketTierReposetory> _mockRepo;
        private readonly Mock<IEventRepository> _mockEventRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TicketTierService _service;

        public TicketTIerTest()
        {
            _mockRepo = new Mock<ITicketTierReposetory>();
            _mockMapper = new Mock<IMapper>();
            _mockEventRepo = new Mock<IEventRepository>();
            _service = new TicketTierService(_mockRepo.Object,_mockEventRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetTicketTierStatistic_ValidData_ReturnsCorrectCalculations()
        {
            var eventId = "event_123";
            var mockTiers = new List<TicketTier>
            {
                new TicketTier { Id = "tier_1", Price = 200, AvailableSeats = 50 },
                new TicketTier { Id = "tier_2", Price = 800, AvailableSeats = 10 }
            };

            _mockRepo.Setup(repo => repo.GetByEventIdAsync(eventId)).ReturnsAsync(mockTiers);

            var result = await _service.GetTicketTierStatistic(eventId);

            Assert.Equal(500, result.AveragePrice); // (200 + 800) / 2 = 500    
            Assert.Equal(60, result.AvailableSeatsCount); // 50 + 10 = 60
            Assert.Equal("tier_2", result.MostExpensiveTierId); 
        }

        [Fact]
        public async Task GetTicketTierStatistic_EmptyCollection_ThrowsInvalidOperationException()
        {
            var eventId = "event_empty";
            _mockRepo.Setup(repo => repo.GetByEventIdAsync(eventId)).ReturnsAsync(new List<TicketTier>()); 

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _service.GetTicketTierStatistic(eventId));

            Assert.Equal("Для даної події не знайдено жодної категорії квитків.", exception.Message);
        }

        [Fact]
        public async Task GetTicketTierStatistic_EmptyEventId_ThrowsArgumentException()
        {
            var invalidEventId = "";

            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.GetTicketTierStatistic(invalidEventId));

            _mockRepo.Verify(repo => repo.GetByEventIdAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task GetTicketTierStatistic_ValidData_CallsRepositoryMethod()
        {
            var eventId = "event_verify";
            _mockRepo.Setup(repo => repo.GetByEventIdAsync(eventId))
                         .ReturnsAsync(new List<TicketTier> { new TicketTier { Id = "test_1", Price = 100, AvailableSeats = 5 } });

            await _service.GetTicketTierStatistic(eventId);

            _mockRepo.Verify(repo => repo.GetByEventIdAsync(eventId), Times.Once);
        }
    }
}
