using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model.Base;
using GeekShopping.ProductAPI.Respository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            IEnumerable<ProductVO> products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            ProductVO product = await _repository.FindById(id);
            if(product is null) return NotFound();
            return Ok(product); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVO product)
        {
            if(product is null) return BadRequest();
            await _repository.Create(product);
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductVO product)
        {
            if (product is null) return BadRequest();
            await _repository.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool status = await _repository.Delete(id);
            if(!status) return BadRequest();
            return Ok(status);
        }
    }
}
