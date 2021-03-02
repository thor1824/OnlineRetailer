using Domane.Model;
using Domane.Model.ServiceFacades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OrderApi.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _serv;

        public OrdersController(IOrderService serv)
        {
            _serv = serv;
        }

        

        // GET orders/5
        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var item = await _serv.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> ByCustomer([FromQuery]int customerId)
        {
            if (customerId == 0) {
                return BadRequest();
            }

            var items = await _serv.GetAllByCustomerAsync(customerId);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        // POST orders
        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> PostAsync([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            var newOrder = await _serv.AddAsync(order);
            return Created("" + newOrder.OrderId, newOrder);
        }

        [HttpPut]
        [Route("[controller]")]
        public async Task<IActionResult> PutAsync([FromBody] Order order)
        {
            try
            {
                await _serv.ChangeStatusAsync(order.OrderId.Value, order.Status.Value);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
