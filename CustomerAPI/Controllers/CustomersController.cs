using Domane.Model;
using Domane.Model.ServiceFacades;
using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _serv;

        public CustomersController(ICustomerService serv)
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
        [HttpGet("{id}", Name = "GetCustomer")]
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
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Customer customer) {
            if (customer == null)
            {
                return BadRequest();
            }
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            return BadRequest();
        }

    }
}
