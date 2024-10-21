using FoodBlazor.Data;
using FoodBlazor.Repository.IRepository;

namespace FoodBlazor.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) { _db = db; }
        public Category Create(Category obj)
        {
            _db.Catagory.Add(obj);
            _db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var obj=_db.Catagory.FirstOrDefault(x => x.Id == id);
            if (obj != null) {
                _db.Catagory.Remove(obj);
                return _db.SaveChanges()>0;
            }
            return false;
        }

        public Category Get(int id)
        {
            var obj = _db.Catagory.FirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                new Category();   
            }
            return obj;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Catagory.ToList();
        }

        public Category Update(Category obj)
        {
            var objFromDb = _db.Catagory.FirstOrDefault(u=>u.Id==obj.Id);
            if (objFromDb != null) {
                objFromDb.Name = obj.Name;
                _db.Catagory.Update(objFromDb);
                _db.SaveChanges();
                return objFromDb;
            }
            return obj;
        }
    }
}
