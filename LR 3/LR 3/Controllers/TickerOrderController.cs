using LR_3.DTO.Event;
using LR_3.DTO.TicketOrder;
using LR_3.Models;
using LR_3.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.IO.File;

namespace LR_3.Controllers
{
    public class TicketOrderDTO
    {
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PromoCode { get; set; }
        public Status? Status { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TickerOrderController : ControllerBase
    {
        //private static List<TicketOrder> orders = new()
        //{
        //    new TicketOrder
        //    {
        //        Id = 1,
        //        customer_email = "nazarkrupitskyi@gmail.com",
        //        customer_phone = "380685168741",
        //        status = Status.Оплачено,

        //    },
        //    new TicketOrder
        //    {
        //        Id = 2,
        //        customer_email = "silleriavotintseva@gmail.com",
        //        customer_phone = "380965432143",
        //        status = Status.Очікування_оплати,
        //    }
        //};

        //public static List<TicketOrder> orders;

        //public const string FilePath = "orders.json";

        //public static void SaveData()
        //{
        //    var options = new JsonSerializerOptions { WriteIndented = true };
        //    string json = JsonSerializer.Serialize(orders, options);

        //    WriteAllText(FilePath, json);
        //}
        //public static List<TicketOrder> LoadData() 
        //{
        //    if (Exists(FilePath))
        //    {
        //        string json = ReadAllText(FilePath);
        //        return JsonSerializer.Deserialize<List<TicketOrder>>(json) ?? new List<TicketOrder>();
        //    }
        //    return new List<TicketOrder>();
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<TicketTier>> GetAll()
        //{
        //    return Ok(orders);
        //}

        //[HttpGet("{id}")]
        //public ActionResult<TicketTier> GetById(int id)
        //{
        //    var order = orders.FirstOrDefault(t => t.Id == id);

        //    if (order == null)
        //        return NotFound($"Квиток з ID={id} не знайдено.");

        //    return Ok(order);
        //}

        //[HttpPost]
        //public ActionResult<TicketOrder> Create([FromBody] TicketOrder neworder)
        //{
        //    var ev = EventController.events.FirstOrDefault(e => e.Id == neworder.EventId);
        //    if (ev == null)
        //    {
        //        return NotFound($"Подію з ID={neworder.EventId} не знайдено.");
        //    }
        //    if (ev.status != EventStatus.Published || ev.startDate.ToUniversalTime() <= DateTime.UtcNow)
        //        return BadRequest("Подія недоступна для бронювання (не опублікована або вже розпочалася).");

        //    var tier = TicketTierController.tickets.FirstOrDefault(t => t.Id == neworder.TierId);
        //    if (tier == null)
        //        return NotFound($"Категорію квитків з ID={neworder.TierId} не знайдено.");

        //    if (tier.AvailableSeats < neworder.RequestedSeats)
        //        return BadRequest($"Недостатньо квитків. Вільних місць залишилось: {tier.AvailableSeats}");

        //    tier.AvailableSeats -= neworder.RequestedSeats;
        //    //TicketTierController.SaveData();

        //    neworder.Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1; 
        //    neworder.CreateTime = DateTime.UtcNow; 

        //    orders.Add(neworder);
        //    //SaveData();

        //    return CreatedAtAction(nameof(GetById), new { id = neworder.Id }, neworder);
        //}
        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] TicketOrder updorder)
        //{
        //    var index = orders.FindIndex(t => t.Id == id);

        //    if (index == -1)
        //        return NotFound($"Заказ з ID={id} не знайдено.");

        //    updorder.Id = id;
        //    orders[index] = updorder;

        //    //SaveData();

        //    return NoContent();
        //}
        //[HttpPatch("{id}")] 
        //public IActionResult Patch(int id, [FromBody] TicketOrderDTO orderPatch)
        //{
        //    var order = orders.FirstOrDefault(o => o.Id == id);

        //    if (order == null)
        //        return NotFound($"Замовлення з ID={id} не знайдено.");

        //    if (orderPatch.CustomerEmail != null)
        //        order.customer_email = orderPatch.CustomerEmail;

        //    if (orderPatch.CustomerPhone != null)
        //        order.customer_phone = orderPatch.CustomerPhone;

        //    if (orderPatch.PromoCode != null)
        //        order.PromoCode = orderPatch.PromoCode;

        //    //SaveData();

        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var order = orders.FirstOrDefault(t => t.Id == id);

        //    if (orders == null)
        //        return NotFound($"Заказ з ID={id} не знайдено.");

        //    orders.Remove(order);

        //    //SaveData();

        //    return NoContent();
        //}
        //[HttpPost("mass-refund/{eventId}")]
        //public IActionResult MassRefund(int eventId)
        //{
        //    var ev = EventController.events.FirstOrDefault(e => e.Id == eventId);
        //    if (ev == null) return NotFound("Подію не знайдено");

        //    if (ev.status == EventStatus.Cancelled || ev.status == EventStatus.Postponed)
        //    {
        //        return BadRequest("Ця подія вже була скасована або перенесена раніше. Повернення вже опрацьовано.");
        //    }

        //    ev.status = EventStatus.Cancelled;
        //    //EventController.SaveData();

        //    var ordersToRefund = orders.Where(o => o.EventId == eventId && o.status == Status.Оплачено).ToList();

        //    foreach (var order in ordersToRefund)
        //    {
        //        order.status = Status.Повернено;

        //        order.TotalAmount = order.TotalAmount - (order.TotalAmount * 0.05m);
        //        order.RefundAmount = order.TotalAmount;

        //        foreach (var ticket in order.Tickets)
        //        {
        //            ticket.IsValid = false;
        //        }


        //    }

        //    //TicketTierController.SaveData();
        //    //SaveData(); 

        //    return Ok($"Подію скасовано. Опрацьовано повернень: {ordersToRefund.Count}");
        //}
        private readonly ITicketOrderService _orderService;

        public TickerOrderController(ITicketOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketOrder>>> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketOrder>> GetById(string id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound($"Замовлення з ID={id} не знайдено.");
            return Ok(order);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] UpdateTicketOrderDTO dto)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(id, dto);
            if (updatedOrder == null) return NotFound($"Замовлення з ID={id} не знайдено.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await _orderService.DeleteOrderAsync(id);
            if (!isDeleted) return NotFound($"Замовлення з ID={id} не знайдено.");

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<TicketOrder>> Create([FromBody] CreateTicketOrderDTO dto)
        {
            try
            {
                var newOrder = await _orderService.CreateOrderAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = newOrder.Id }, newOrder);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); 
            }
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> Pay(string id)
        {
            try
            {
                var paidOrder = await _orderService.PayOrderAsync(id);
                return Ok(paidOrder);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("mass-refund/{eventId}")]
        public async Task<IActionResult> MassRefund(string eventId)
        {
            try
            {
                var resultMessage = await _orderService.MassRefundAsync(eventId);
                return Ok(new { message = resultMessage });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
      
        }
    }
}

