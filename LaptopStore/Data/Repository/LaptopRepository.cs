using LaptopStore.Data.Interfaces;
using LaptopStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopStore.Data.Repository
{
    public class LaptopRepository : ILaptops
    {
        private readonly AppDBContent _db;
        public LaptopRepository(AppDBContent db)
        {
            this._db = db;
        }

        public IQueryable<Laptop> GetFavLaptops() => 
            _db.Laptops.Where(p => p.isFavorite);

        public IQueryable<Laptop> GetAll()
        {
            return _db.Laptops;
        }

        public IQueryable<Laptop> GetLaptopsByCategory(Category category)
        {
            return _db.Laptops.Where(c => c.Category == category);
        }

        public Laptop GetObjectLaptop(long laptopId) =>
            _db.Laptops.FirstOrDefault(p => p.id == laptopId);

        public async Task Create(Laptop laptop)
        {
            await _db.Laptops.AddAsync(laptop);
            await _db.SaveChangesAsync();
        }

        public Task Delete(Laptop entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Laptop> Update(Laptop entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
