using FluentValidation;

namespace D1.Model.Authentification
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }

    }


    public class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
    {
        public ForgotPasswordModelValidator()
        {
            RuleFor(model => model.Email).NotEmpty().EmailAddress();
        }
    }
}
