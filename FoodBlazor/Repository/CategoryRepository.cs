using FoodBlazor.Data;
using FoodBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FoodBlazor.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) { _db = db; }
        public async Task<Category> CreateAsync(Category obj)
        {
            await _db.Catagory.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj=await _db.Catagory.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null) {
                _db.Catagory.Remove(obj);
                return (await _db.SaveChangesAsync())>0;
            }
            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var obj =await _db.Catagory.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
            {
                new Category();   
            }
            return obj;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Catagory.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            var objFromDb =await _db.Catagory.FirstOrDefaultAsync(u=>u.Id==obj.Id);
            if (objFromDb != null) {
                objFromDb.Name = obj.Name;
                _db.Catagory.Update(objFromDb);
                await _db.SaveChangesAsync();
                return objFromDb;
            }
            return obj;
        }
    }
}
