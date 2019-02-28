using FluentValidation;


namespace D1.Model
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(model => model.Login).NotEmpty().EmailAddress();
            RuleFor(model => model.Password).NotEmpty();
           
        }
    }

}



