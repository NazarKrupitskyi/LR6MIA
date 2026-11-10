using AutoMapper;
using LR_3.DBCommunication.Documents;
using LR_3.DBCommunication.Repositories.Interfaces;
using LR_3.DTO.Event;
using LR_3.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LR_3.DBCommunication.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<EventDocument> _collection;
        private readonly IMapper _mapper;

        public EventRepository(IMongoDatabase db, IMapper mapper)
        {
            _collection = db.GetCollection<EventDocument>("events");
            _mapper = mapper;
        }

        public async Task CreateAsync(Event eventModel)
        {
            var document = _mapper.Map<EventDocument>(eventModel);

            await _collection.InsertOneAsync(document);

            eventModel.Id = document.Id;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            var documents = await _collection.Find(_ => true).ToListAsync();
            return _mapper.Map<List<Event>>(documents);
        }

        public async Task<Event?> GetByIdAsync(string id)
        {
            var document = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return document is null ? null : _mapper.Map<Event>(document);
        }

        public async Task UpdateAsync(Event eventModel)
        {
            var document = _mapper.Map<EventDocument>(eventModel);
            await _collection.ReplaceOneAsync(x => x.Id == eventModel.Id, document);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }
        //private static Event ToModel(EventDocument document) => new()
        //{
        //    Id = document.Id,
        //    Title = document.Title,
        //    Description = document.Description,
        //    location = document.location,
        //    startDate = document.startDate,
        //    status = document.status,
        //};
    }

}
