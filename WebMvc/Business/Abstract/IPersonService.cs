using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPersonService
    {
        IResult Add(Person person);
        Person GetByMail(string mail);
    }
}
