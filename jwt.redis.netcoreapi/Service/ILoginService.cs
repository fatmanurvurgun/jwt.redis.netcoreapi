using jwt.redis.netcoreapi.Models.Request;
using jwt.redis.netcoreapi.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Service
{
    public interface ILoginService
    {
        BaseResponse<LoginResponseModel> Login(LoginRequestModel loginRequestModel);

    }
}
