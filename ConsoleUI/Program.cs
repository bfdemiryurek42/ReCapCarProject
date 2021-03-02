using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entity.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarTest();
            //AddCustomer();

        }

        private static void AddCustomer()
        {
            CustomerManager customer1 = new CustomerManager(new EfCustomerDal());
            customer1.Add(new Customer
            {
                UserId = 1,
                CompanyName = "Stechome"
            });
        }

        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(car.CarName + "/" + car.BrandName);
            }
        }
    }
}
