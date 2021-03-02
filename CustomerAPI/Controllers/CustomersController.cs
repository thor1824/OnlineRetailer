using Domane.Model;
using Domane.Model.ServiceFacades;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var item = await _serv.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST orders
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            var newCust = await _serv.AddAsync(customer);
            return Created(newCust.CustomerId + "", newCust);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            await _serv.UpdateAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            await _serv.DeleteAsync(id);
            return NoContent();
        }

    }
}
