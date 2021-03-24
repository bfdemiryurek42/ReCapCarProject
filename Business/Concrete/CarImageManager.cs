using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(IFormFile formFile,CarImage carImage)
        {
            IResult result = BusinessRules.Run(
                    CheckIfImageCountLimit(carImage.CarId), CheckIfImageExtensionValid(formFile)
                    ) ;

            if (result != null)
            {
                return result;
            }

            carImage.ImagePath = FileHelper.Add(formFile);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            IResult result = BusinessRules.Run(
                 FileHelper.Delete(_carImageDal.GetById(p => p.Id == carImage.Id).ImagePath));

            if (result != null)
            {
                return result;
            }

            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.GetById(c=> c.Id == id));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            List<CarImage> carImages = new List<CarImage>();
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result == 0)
            {
                CarImage c = new CarImage { CarId = carId, Date = DateTime.Now, ImagePath = @"\images\default.jpg" };
                carImages.Add(c);
                return new SuccessDataResult<List<CarImage>>(carImages);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
        }

        public IResult Update(IFormFile formFile,CarImage carImage)
        {
            IResult result = BusinessRules.Run(
                CheckIfImageCountLimit(carImage.CarId)
                );

            if (result != null)
            {
                return result;
            }
            carImage.ImagePath = FileHelper.Update(_carImageDal.GetById(p => p.Id == carImage.Id).ImagePath, formFile);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }
        private IResult CheckIfImageCountLimit(int CarId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == CarId).Count;
            if (result >= 5)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
        private IResult CheckIfImageExtensionValid(IFormFile file)
        {
            string[] ValidImageFileTypes = { ".JPG", ".JPEG", ".PNG", ".TIF", ".TIFF", ".GIF", ".BMP", ".ICO", "JFIF" };
            bool isValidFileExtension = ValidImageFileTypes.Any(t => t == Path.GetExtension(file.FileName).ToUpper());
            if (!isValidFileExtension)
                return new ErrorResult("Resim formatında değil");
            return new SuccessResult();
        }

    }
}

