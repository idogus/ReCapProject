using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class MemCarDal : ICarDal
    {
        private List<Car> _cars;
        public MemCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id = 1, BrandId = 1, ColorId = 3, DailyPrice= 450, ModelYear=2017, Description="Klima, Dokunmatik Media Sistemi"},
                new Car{Id = 2, BrandId = 2, ColorId = 3, DailyPrice= 500, ModelYear=2018, Description="Klima, Dokunmatik Media Sistemi"},
                new Car{Id = 3, BrandId = 2, ColorId = 1, DailyPrice= 700, ModelYear=2018, Description="Klima, Dokunmatik Media Sistemi"},
                new Car{Id = 4, BrandId = 3, ColorId = 1, DailyPrice= 1250, ModelYear=2020, Description="Klima, Dokunmatik Media Sistemi"},
                new Car{Id = 5, BrandId = 3, ColorId = 2, DailyPrice= 1200, ModelYear=2020, Description="Klima, Dokunmatik Media Sistemi"},
            };
        }
        public void Add(Car entity)
        {
            entity.Id = _cars.Max(x => x.Id) + 1; // cars nesnesindeki Id numaraları tek artımlı olarak yaratan code satırı.
            _cars.Add(entity);
        }

        public void Delete(Car entity)
        {
            _cars.Remove(entity);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            return _cars.SingleOrDefault(filter.Compile());
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return filter == null ? _cars : _cars.Where(filter.Compile()).ToList(); //metod parametresinde filtre gelip gelmediğini kontrol eden Ternary (tek satır if) koşulu
        }

        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(c => c.Id == id);
        }

        public void Update(Car entity)
        {
            var car = _cars.SingleOrDefault(c => c.Id == entity.Id);
            if (car == null) throw new NullReferenceException("Güncellenecek araç bulunamadı!");
            car.BrandId = entity.BrandId;
            car.ColorId = entity.ColorId;
            car.DailyPrice = entity.DailyPrice;
            car.ModelYear = entity.ModelYear;
            car.Description = entity.Description;
        }
    }
}
