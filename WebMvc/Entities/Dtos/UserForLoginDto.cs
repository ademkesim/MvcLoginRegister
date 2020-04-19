using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities;
using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class UserForLoginDto : IDto
    {
        [Required(ErrorMessage = "Lütfen E Posta Adresinizi Giriniz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
