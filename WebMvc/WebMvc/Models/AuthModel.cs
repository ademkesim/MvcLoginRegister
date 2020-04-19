using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class AuthModel
    {
        public UserForLoginDto userForLoginDto { get; set; }
        public UserForRegisterDto userForRegisterDto { get; set; }
    }
}
