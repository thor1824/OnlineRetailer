﻿using Domane.Model;
using Domane.Model.ServiceFacades;
using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _serv;

        public OrdersController(IOrderService serv)
        {
            _serv = serv;
        }

        //// GET: orders
        //[HttpGet]
        //public IEnumerable<Order> Get()
        //{
        //    return _serv.GetAll();
        //}

        // GET orders/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var item = _serv.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST orders
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            //// Call ProductApi to get the product ordered
            //RestClient c = new RestClient();
            //// You may need to change the port number in the BaseUrl below
            //// before you can run the request.
            //c.BaseUrl = new Uri("https://localhost:5001/products/");
            //var request = new RestRequest(order.ProductId.ToString(), Method.GET);
            //var response = c.Execute<Product>(request);
            //var orderedProduct = response.Data;

            //if (order.Quantity <= orderedProduct.ItemsInStock - orderedProduct.ItemsReserved)
            //{
            //    // reduce the number of items in stock for the ordered product,
            //    // and create a new order.
            //    orderedProduct.ItemsReserved += order.Quantity;
            //    var updateRequest = new RestRequest(orderedProduct.Id.ToString(), Method.PUT);
            //    updateRequest.AddJsonBody(orderedProduct);
            //    var updateResponse = c.Execute(updateRequest);

            //    if (updateResponse.IsSuccessful)
            //    {
            //        var newOrder = _serv.Add(order);
            //        return CreatedAtRoute("GetOrder", new { id = newOrder.Id }, newOrder);
            //    }
            //}

            //// If the order could not be created, "return no content".
            return NoContent();
        }

    }
}
