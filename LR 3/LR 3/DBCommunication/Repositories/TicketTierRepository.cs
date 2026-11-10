using AutoMapper;
using LR_3.DBCommunication.Documents;
using LR_3.DBCommunication.Repositories.Interfaces;
using LR_3.DTO.Event;
using LR_3.DTO.TicketTier;
using LR_3.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LR_3.DBCommunication.Repositories
{
    public class TicketTierRepository : ITicketTierReposetory
    {
        private readonly IMongoCollection<TicketTierDocument> _collection;
        private readonly IMapper _mapper;

        public TicketTierRepository(IMongoDatabase db, IMapper mapper)
        {
            _collection = db.GetCollection<TicketTierDocument>("tickets");
            _mapper = mapper;
        }

        public async Task CreateAsync(TicketTier ticketTier)
        {
            var document = _mapper.Map<TicketTierDocument>(ticketTier);

            await _collection.InsertOneAsync(document);

            ticketTier.Id = document.Id;
        }

        public async Task<List<TicketTier>> GetAllAsync()
        {
            var documents = await _collection.Find(_ => true).ToListAsync();
            return _mapper.Map<List<TicketTier>>(documents);
        }

        public async Task<TicketTier?> GetByIdAsync(string id)
        {
            var document = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return document is null ? null : _mapper.Map<TicketTier>(document);
        }

        public async Task UpdateAsync(TicketTier ticketTier)
        {
            var document = _mapper.Map<TicketTierDocument>(ticketTier);
            await _collection.ReplaceOneAsync(x => x.Id == ticketTier.Id, document);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<TicketTier>> GetByEventIdAsync(string eventId)
        {
            var documents = await _collection.Find(x => x.EventId == eventId).ToListAsync();

            return _mapper.Map<List<TicketTier>>(documents);
        }
    }
}
