using D1.Model;
using D1.Model.Authentification;
using D1.Model.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;


namespace WebApi.Controllers
{
    public class AuthController:Controller
    {
        private readonly IAuthService _authService;
    

        public AuthController(IAuthService authService)
        {
            _authService = authService;
      
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(Routes.Login)]
        public ApiResponse<TokenModel> Login([FromBody]LoginModel loginModel)
        {
            
            TokenModel token = _authService.Login(loginModel);
            return new ApiResponse<TokenModel>(token);
            
        }


        [HttpPost]
        [AllowAnonymous]
        [Route(Routes.ForgotPassword)]
        public ApiResponse ForgotPassword([FromBody] ForgotPasswordModel email)
        {
           _authService.ForgotPassword(email);        
            return new ApiResponse<TokenModel>(new TokenModel());
        }


        
        [HttpPost]
        [AllowAnonymous]
        [Route(Routes.RecoverPassword)]
        public ApiResponse RecoverPassword([FromBody] RecoverPasswordModel recoverPasswordModel)
        {
            _authService.RecoverPassword(recoverPasswordModel);
            return new ApiResponse();
        }
    }
}