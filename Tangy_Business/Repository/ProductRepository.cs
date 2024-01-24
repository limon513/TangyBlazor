using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models;

namespace Tangy_Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ApplicationDbContext _db;
        public IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper) {
            _db=db;
            _mapper=mapper;
        }
        public async Task<ProductDTO> Create(ProductDTO productDTO)
        {
            var obj = _mapper.Map<ProductDTO, Product>(productDTO);
            var addedObj = _db.Products.Add(obj);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(obj != null)
            {
                _db.Products.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
            
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(u=>u.Category));
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var obj = await _db.Products.Include(u=> u.Category).FirstOrDefaultAsync(p => p.Id==id);
            if(obj != null)
            {
                return _mapper.Map<Product,ProductDTO>(obj);
            }
            return new ProductDTO();
        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            var obj = await _db.Products.FirstOrDefaultAsync(p=> p.Id==productDTO.Id);
            if(obj != null)
            {
                obj.Name = productDTO.Name;
                obj.Description = productDTO.Description;
                obj.CustomerFavourite = productDTO.CustomerFavourite;
                obj.ShopFavourtie = productDTO.ShopFavourtie;
                obj.Color = productDTO.Color;
                obj.ImageUrl = productDTO.ImageUrl;
                obj.CategoryId = productDTO.CategoryId;
                _db.Products.Update(obj);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(obj);
            }
            return productDTO;
        }
    }
}
