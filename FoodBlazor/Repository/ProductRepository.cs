﻿using FoodBlazor.Data;
using FoodBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FoodBlazor.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment) { 
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Product> CreateAsync(Product obj)
        {
            await _db.Product.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj=await _db.Product.FirstOrDefaultAsync(x => x.Id == id);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            if (obj != null) {
                _db.Product.Remove(obj);
                return (await _db.SaveChangesAsync())>0;
            }
            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            var obj =await _db.Product.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
            {
                new Product();   
            }
            return obj;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            //return await _db.Product.Include(u=>u.Category).ToListAsync();
            var products = await (from p in _db.Product
                                  join c in _db.Catagory on p.CategoryId equals c.Id into joined
                                  from c in joined.DefaultIfEmpty()
                                  select new Product
                                  {
                                      Id = p.Id,
                                      Name = p.Name,
                                      Price = p.Price,
                                      Description = p.Description,
                                      SpecialTag = p.SpecialTag,
                                      CategoryId = p.CategoryId,
                                      Category = c,
                                      ImageUrl = p.ImageUrl
                                  }).ToListAsync();
            return products;
        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            var objFromDb =await _db.Product.FirstOrDefaultAsync(u=>u.Id==obj.Id);
            if (objFromDb != null) {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.ImageUrl = obj.ImageUrl;
                _db.Product.Update(objFromDb);
                await _db.SaveChangesAsync();
                return objFromDb;
            }
            return obj;
        }
    }
}
