using Core.DataAccess.EntityFramewok;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapCarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapCarContext context = new ReCapCarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join cl in context.Colors
                             on c.ColorId equals cl.ColorId
                             select new CarDetailDto { 
                                 Id = c.Id, BrandId=c.BrandId, ColorId=c.ColorId, CarName = c.CarName, BrandName = b.BrandName, ColorName = cl.ColorName,  Description = c.Description, DailyPrice = c.DailyPrice, ModelYear=c.ModelYear
                             };

                return result.ToList();
            }
        }


        public List<CarDetailDto> GetCarDetailsById(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapCarContext context = new ReCapCarContext())
            {
                var result =
                             from car in filter == null ? context.Cars : context.Cars.Where(filter)
                             join br in context.Brands
                              on car.BrandId equals br.BrandId
                             join col in context.Colors
                            on car.ColorId equals col.ColorId

                             select new CarDetailDto
                             {
                                 Id = car.Id,
                                 BrandName = br.BrandName,
                                 ColorName = col.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 BrandId = car.BrandId,
                                 ColorId = car.ColorId
                                 //ImagePath = @"\images\default.jfif",
                                 //CarImages = new List<CarImages>()
                             };

                return result.ToList();


            }
        }
        public CarDetailDto GetCarById(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapCarContext context = new ReCapCarContext())
            {
                var result =
                             from car in filter == null ? context.Cars : context.Cars.Where(filter)
                             join br in context.Brands
                              on car.BrandId equals br.BrandId
                             join col in context.Colors
                            on car.ColorId equals col.ColorId

                             select new CarDetailDto
                             {
                                 Id = car.Id,
                                 BrandName = br.BrandName,
                                 ColorName = col.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 BrandId = car.BrandId,
                                 ColorId = car.ColorId
                                 //ImagePath = @"\images\default.jfif",
                                 //CarImages = new List<CarImages>()
                             };

                return result.FirstOrDefault();


            }
        }
    }
}
