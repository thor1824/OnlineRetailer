using Microsoft.AspNetCore.Mvc;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using System.Threading.Tasks;

namespace Or.Micro.Customers.Controllers
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
            try
            {
                var item = await _serv.GetAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST orders
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }
                var newCust = await _serv.AddAsync(customer);
                return Created(newCust.CustomerId + "", newCust);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }
                await _serv.UpdateAsync(customer);
                return NoContent();
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                await _serv.DeleteAsync(id);
                return NoContent();
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
