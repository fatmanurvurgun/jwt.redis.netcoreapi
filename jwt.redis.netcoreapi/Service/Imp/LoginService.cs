using jwt.redis.netcoreapi.Data;
using jwt.redis.netcoreapi.Models.Request;
using jwt.redis.netcoreapi.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Service.Imp
{
    public class LoginService : ILoginService
    {
        private readonly IDataManager _dataManager;

        public LoginService(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public BaseResponse<LoginResponseModel> Login(LoginRequestModel loginRequestModel)
        {
            BaseResponse<LoginResponseModel> baseResponse = new BaseResponse<LoginResponseModel>();

            var result = _dataManager.Login(loginRequestModel);

            if (result == null)
            {
                baseResponse.Errors.Add("Email Yada Şifre Yanlış");
                return baseResponse;
            }

            baseResponse.Data = result;
            return baseResponse;
        }
    }
}
