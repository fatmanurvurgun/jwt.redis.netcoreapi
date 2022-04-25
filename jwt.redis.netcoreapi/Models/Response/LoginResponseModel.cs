using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Models.Response
{
    public class LoginResponseModel
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
