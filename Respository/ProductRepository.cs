using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model.Base;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GeekShopping.ProductAPI.Respository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            Product product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO productVo)
        {
            Product product = _mapper.Map<Product>(productVo);
            _context.Add(product);
            await _context.SaveChangesAsync(); 
            return productVo;
        }

        public async Task<ProductVO> Update(ProductVO productVo)
        {
            Product product = _mapper.Map<Product>(productVo);
            _context.Update(product);
            await _context.SaveChangesAsync();
            return productVo;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                Product product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();

                if (product is null) return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
