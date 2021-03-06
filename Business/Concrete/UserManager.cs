﻿using Business.Abstract;
using Business.Constants;
using Core.Entities.Conrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userdal;

        public UserManager(IUserDal userdal)
        {
            _userdal = userdal;
        }

        public IResult Add(User user)
        {

            _userdal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(User user)
        {
            _userdal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userdal.GetAll(),Messages.UsersListed);
        }

        public IDataResult<List<User>> GetUsersById(int id)
        {
            return new SuccessDataResult<List<User>>(_userdal.GetAll(c=>c.Id == id).ToList());
        }

        public IResult Update(User user)
        {
            _userdal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
        public List<OperationClaim> GetClaims(User user)
        {
            return _userdal.GetClaims(user);
        }

        public User GetByMail(string email)
        {
            return _userdal.GetById(u => u.Email == email);
        }
    }
}
