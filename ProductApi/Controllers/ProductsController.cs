using Domane.Model.ServiceFacades;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _serv;

        public ProductsController(IProductService serv)
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
                return Ok(item);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                return Ok(await _serv.GetAllAsync());

            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        

        //// POST products
        //[HttpPost]
        //public IActionResult Post([FromBody]Product product)
        //{
        //    if (product == null)
        //    {
        //        return BadRequest();
        //    }

        //    var newProduct = _serv.Add(product);

        //    return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);
        //}

        //// PUT products/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody]Product product)
        //{
        //    if (product == null || product.Id != id)
        //    {
        //        return BadRequest();
        //    }

        //    var modifiedProduct = _serv.Get(id);

        //    if (modifiedProduct == null)
        //    {
        //        return NotFound();
        //    }

        //    modifiedProduct.Name = product.Name;
        //    modifiedProduct.Price = product.Price;
        //    modifiedProduct.ItemsInStock = product.ItemsInStock;
        //    modifiedProduct.ItemsReserved = product.ItemsReserved;

        //    _serv.Edit(modifiedProduct);
        //    return new NoContentResult();
        //}

        //// DELETE products/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    if (_serv.Get(id) == null)
        //    {
        //        return NotFound();
        //    }

        //    _serv.Remove(id);
        //    return new NoContentResult();
        //}
    }
}
