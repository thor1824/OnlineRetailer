﻿using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Or.Micro.Orders.Models;
using Or.Micro.Orders.Services;
using System;
using System.Threading.Tasks;

namespace Or.Micro.Orders.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _serv;
        private readonly IBus _bus;

        public OrdersController(IOrderService serv, IBus bus)
        {
            _serv = serv;
            _bus = bus;
        }



        // GET orders/5
        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var item = await _serv.GetAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> ByCustomer([FromQuery] int customerId)
        {
            try
            {
                if (customerId == 0)
                {
                    return BadRequest();
                }

                var items = await _serv.GetAllByCustomerAsync(customerId);
                if (items == null)
                {
                    return NotFound();
                }
                return Ok(items);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST orders
        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> PostAsync([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest();
                }
                var newOrder = await _serv.AddAsync(order);
                return Created("" + newOrder.OrderId, newOrder);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
