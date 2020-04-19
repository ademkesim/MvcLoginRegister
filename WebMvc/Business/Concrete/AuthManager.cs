using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Hashing;
using Core.Security.Jwt;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IPersonService _personService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IPersonService personService, ITokenHelper tokenHelper)
        {
            _personService = personService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<Person> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new Person()
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Mail = userForRegisterDto.Mail,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _personService.Add(user);
            return new SuccessDataResult<Person>(user, "Kayıt başarılı!");
        }

        public IDataResult<Person> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _personService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<Person>("Kullanıcı bulunamadı");
            }

            if (HashingHelper.VeryfPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,
                userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<Person>("Şifre Hatalı");

            }

            return new SuccessDataResult<Person>(userToCheck, "Giriş başarılı!");


        }

        public IResult UserExists(string email)
        {
            if (_personService.GetByMail(email) != null)
            {
                return new ErrorResult("Kayıt bulunamadı!");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(Person person)
        {

            var accessToken = _tokenHelper.CreateToken(person);
            return new SuccessDataResult<AccessToken>(accessToken, "token oluşturuldu");
        }
    }
}
