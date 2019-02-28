using D1.Model.Authentification;

namespace D1.Model.Services.Abstract
{
    public interface IAuthService
    {
        TokenModel Login(LoginModel loginModel);

        void ForgotPassword(ForgotPasswordModel email);

        void RecoverPassword(RecoverPasswordModel model);
    
    }
}
