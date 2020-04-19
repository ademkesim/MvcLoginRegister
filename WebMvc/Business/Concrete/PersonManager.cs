using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class PersonManager:IPersonService
    {
        private IUserDal _userDal;
        public IResult Add(Person person)
        {
            _userDal.Add(person);
            return new SuccessResult("Kişi  Eklendi");
        }
        public Person GetByMail(string mail)
        {
            return _userDal.Get(u => u.Mail == mail);
        }
    }
}
