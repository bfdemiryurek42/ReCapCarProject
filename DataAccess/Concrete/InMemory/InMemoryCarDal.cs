using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id=1, BrandId=1, ColorId=1, DailyPrice=100, ModelYear=2015, Description="Car1"},
                new Car{Id=2, BrandId=1, ColorId=2, DailyPrice=150, ModelYear=2014, Description="Car2"},
                new Car{Id=3, BrandId=2, ColorId=3, DailyPrice=170, ModelYear=2016, Description="Car3"},
                new Car{Id=4, BrandId=2, ColorId=4, DailyPrice=110, ModelYear=2012, Description="Car4"},
                new Car{Id=5, BrandId=2, ColorId=3, DailyPrice=120, ModelYear=2017, Description="Car5"},
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            var carsToBeDeleted = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carsToBeDeleted);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetById(int Id)
        {
            return _cars.Where(c => c.Id == Id).ToList();
        }

        public void Update(Car car)
        {
            var carsToBeUpdated = _cars.SingleOrDefault(c => c.Id == car.Id);
            carsToBeUpdated.BrandId = car.BrandId;
            carsToBeUpdated.ColorId = car.ColorId;
            carsToBeUpdated.DailyPrice = car.DailyPrice;
            carsToBeUpdated.Description = car.Description;
        }
    }
}
