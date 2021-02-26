using Domane.Model;
using Domane.Model.ServiceFacades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        // GET products
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _serv.GetAll();
        }

        // GET products/5
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get(int id)
        {
            var item = _serv.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
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
