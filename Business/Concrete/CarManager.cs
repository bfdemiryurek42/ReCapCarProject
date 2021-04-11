using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
                _carDal.Add(car);
                return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            //if (DateTime.Now.Hour==22)
            //{
            //    return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<CarDetailDto> GetById(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarById(c => c.Id == carId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<CarDetailDto> GetCarDetailsById(int id)
        {
            //CarDetailDto carDetailDto = _carDal.GetCarDetailsById(c => c.Id == id);
            //carDetailDto.CarImages = _carImagesService.GetAllByCarId(id).Data;
            return new SuccessDataResult<CarDetailDto>();
        }

        public IDataResult<List<CarDetailDto>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsById(c => c.BrandId == brandId).ToList());
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsById(c => c.ColorId == colorId).ToList());
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
             _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);


        }

    }
}
