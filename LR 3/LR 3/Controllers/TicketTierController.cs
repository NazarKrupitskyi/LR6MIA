using LR_3.Controllers;
using LR_3.DTO.TicketTier;
using LR_3.Models;
using LR_3.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.IO.File;

namespace LR_3.Controllers
{
    public class DTOTicketTierPatch
    {
        public int? EventId { get; set; } 
        public TicketCategory? Category { get; set; }
        public int? Price { get; set; }
        public int? AvailableSeats { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TicketTierController : ControllerBase
    {
        //private static List<TicketTier> tickets = new()
        //{
        //    new TicketTier
        //    {
        //        Id = 1,
        //        EventId = 1,
        //        Category = TicketCategory.ВІП,
        //        Price = 2600,
        //        Quantity = 0
        //    },
        //    new TicketTier
        //    {
        //        Id = 2,
        //        EventId = 2,
        //        Category = TicketCategory.Стандарт,
        //        Price = 100,
        //        Quantity = 200
        //    }
        //};

        //public static List<TicketTier> tickets;

        //public const string FilePath = "tickets.json";

        //public static void SaveData()
        //{
        //    var options = new JsonSerializerOptions { WriteIndented = true };
        //    string json = JsonSerializer.Serialize(tickets, options);

        //    WriteAllText(FilePath, json);
        //}
        //public static List<TicketTier> LoadData() // з цим питання
        //{
        //    if (Exists(FilePath))
        //    {
        //        string json = ReadAllText(FilePath);
        //        return JsonSerializer.Deserialize<List<TicketTier>>(json) ?? new List<TicketTier>();
        //    }
        //    return new List<TicketTier>();
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<TicketTier>> GetAll()
        //{
        //    return Ok(tickets);
        //}

        //[HttpGet("{id}")]
        //public ActionResult<TicketTier> GetById(int id)
        //{
        //    var ticket = tickets.FirstOrDefault(t => t.Id == id);

        //    if (ticket == null)
        //        return NotFound($"Квиток з ID={id} не знайдено.");

        //    return Ok(ticket);
        //}

        //[HttpPost]
        //public ActionResult<TicketTier> Create([FromBody] TicketTier newticket)
        //{
        //    var eventExists = EventController.events.Any(e => e.Id == newticket.EventId);
        //    if (!eventExists)
        //    {
        //        return NotFound($"Подію з ID={newticket.EventId} не знайдено.");
        //    }
        //    newticket.Id = tickets.Count + 1;

        //    tickets.Add(newticket);

        //    //SaveData();

        //    return CreatedAtAction(nameof(GetById), new { id = newticket.Id }, newticket);
        //}
        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] TicketTier updticket)
        //{
        //    var index = tickets.FindIndex(t => t.Id == id);

        //    if (index == -1)
        //        return NotFound($"Квиток з ID={id} не знайдено.");

        //    updticket.Id = id;
        //    tickets[index] = updticket;

        //    //SaveData();

        //    return NoContent();
        //}
        //[HttpPatch("{id}")] 
        //public IActionResult Patch(int id, [FromBody] DTOTicketTierPatch tierPatch)
        //{
        //    var tier = tickets.FirstOrDefault(t => t.Id == id);

        //    if (tier == null)
        //        return NotFound($"Категорію квитків з ID={id} не знайдено.");

        //    if (tierPatch.EventId != null)
        //    {
        //        var eventExists = EventController.events.Any(e => e.Id == tierPatch.EventId.Value);
        //        if (!eventExists)
        //            return BadRequest($"Неможливо прив'язати квитки. Подію з ID={tierPatch.EventId.Value} не знайдено.");

        //        tier.EventId = tierPatch.EventId.Value;
        //    }

        //    if (tierPatch.EventId != null)
        //        tier.EventId = tierPatch.EventId.Value;

        //    if (tierPatch.Category != null)
        //        tier.Category = tierPatch.Category.Value;

        //    if (tierPatch.Price != null)
        //        tier.Price = tierPatch.Price.Value;

        //    if (tierPatch.AvailableSeats != null)
        //        tier.AvailableSeats = tierPatch.AvailableSeats.Value;

        //    //SaveData();

        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var ticket = tickets.FirstOrDefault(t => t.Id == id);

        //    if (tickets == null)
        //        return NotFound($"Квиток з ID={id} не знайдено.");

        //    tickets.Remove(ticket);

        //    //SaveData();

        //    return NoContent();
        //}



        //[HttpPost("{id}/pay")]
        //public IActionResult Pay(int id)
        //{
        //    var order = TickerOrderController.orders.FirstOrDefault(o => o.Id == id);
        //    if (order == null) return NotFound();
        //    if (order.status != Status.Очікування_оплати) return BadRequest("Замовлення не очікує оплати.");

        //    var tier = TicketTierController.tickets.FirstOrDefault(t => t.Id == order.TierId);

        //    if ((DateTime.UtcNow - order.CreateTime).TotalMinutes > 15)
        //    {
        //        order.status = Status.Відхилено;
        //        tier.AvailableSeats += order.RequestedSeats; 

        //        //TicketTierController.SaveData();
        //        //SaveData(); 

        //        return BadRequest("Час на оплату вичерпано. Бронь скасовано.");
        //    }


        //    decimal basePrice = tier.Price * order.RequestedSeats;
        //    decimal discount = (order.PromoCode == "Студент" && tier.Category == TicketCategory.Стандарт)
        //        ? basePrice * 0.15m : 0;

        //    decimal ticketsCost = basePrice - discount;
        //    order.RefundAmount = ticketsCost;
        //    order.TotalAmount = ticketsCost + (ticketsCost * 0.05m);

        //    Random random = new Random();

        //    for (int i = 0; i < order.RequestedSeats; i++)
        //    {
        //        order.Tickets.Add(new DigitalTicket
        //        {
        //            Barcode = random.Next(1000,9999).ToString(),
        //            IsValid = true
        //        });
        //    }

        //    order.status = Status.Оплачено;
        //    //SaveData();

        //    return Ok(order);
        //}
        private readonly ITicketTierService _tierService;

        public TicketTierController(ITicketTierService tierService)
        {
            _tierService = tierService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketTier>>> GetAll()
        {
            var tiers = await _tierService.GetAllTiersAsync();
            return Ok(tiers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketTier>> GetById(string id)
        {
            var tier = await _tierService.GetTierByIdAsync(id);
            if (tier == null) return NotFound($"Категорію квитків з ID={id} не знайдено.");
            return Ok(tier);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TicketTier>> Create([FromBody] CreateTicketTierDTO dto)
        {
            try
            {
                var createdTier = await _tierService.CreateTierAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdTier.Id }, createdTier);
            }
            catch (ArgumentException ex) 
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] UpdateTicketTierDTO dto)
        {
            var updatedTier = await _tierService.UpdateTierAsync(id, dto);
            if (updatedTier == null) return NotFound($"Категорію квитків з ID={id} не знайдено.");

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await _tierService.DeleteTierAsync(id);
            if (!isDeleted) return NotFound($"Категорію квитків з ID={id} не знайдено.");

            return NoContent();
        }
    }
}
