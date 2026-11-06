using AutoMapper;
using LR_3.DBCommunication.Documents;
using LR_3.DBCommunication.Repositories.Interfaces;
using LR_3.DTO.TicketOrder;
using LR_3.DTO.TicketTier;
using LR_3.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LR_3.DBCommunication.Repositories
{
    public class TicketOrderRepository : ITicketOrderReposetory
    {
        private readonly IMongoCollection<TicketOrderDocument> _collection;
        private readonly IMapper _mapper;

        public TicketOrderRepository(IMongoDatabase db, IMapper mapper)
        {
            _collection = db.GetCollection<TicketOrderDocument>("orders");
            _mapper = mapper;
        }

        public async Task<List<TicketOrder>> GetAllAsync()
        {
            var documents = await _collection.Find(_ => true).ToListAsync();
            return _mapper.Map<List<TicketOrder>>(documents);
        }
        public async Task<TicketOrder?> GetByIdAsync(string id)
        {
            var document = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return document is null ? null : _mapper.Map<TicketOrder>(document);
        }
        public async Task CreateAsync(TicketOrder ticketOrder)
        {
            var document = _mapper.Map<TicketOrderDocument>(ticketOrder);

            await _collection.InsertOneAsync(document);

            ticketOrder.Id = document.Id;
        }
        
        public async Task UpdateAsync(TicketOrder ticketOrder)
        {
            var document = _mapper.Map<TicketOrderDocument>(ticketOrder);
            await _collection.ReplaceOneAsync(x => x.Id == ticketOrder.Id, document);
        }
        public async Task DeleteAsync(string id)  
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }
        //private static TicketOrder ToModel(TicketOrderDocument document) => new()
        //{
        //    Id = document.Id,
        //    EventId = document.EventId,
        //    TierId = document.TierId,
        //    customer_email= document.customer_email,
        //    customer_phone = document.customer_phone,
        //    RequestedSeats = document.RequestedSeats,
        //    PromoCode = document.PromoCode,
        //    status = document.status,
        //    CreateTime = document.CreateTime,
        //    TotalAmount = document.TotalAmount,
        //    RefundAmount = document.RefundAmount,
        //    Tickets = document.Tickets,
        //};
    }
}
