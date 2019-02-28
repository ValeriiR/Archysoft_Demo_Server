using FluentValidation;

namespace D1.Model.Authentification
{
    public class RecoverPasswordModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class RecoverPasswordModelValidator : AbstractValidator<RecoverPasswordModel>
    {
        public RecoverPasswordModelValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.Password)
                .Equal(x => x.PasswordConfirm).NotNull().NotEmpty().Length(6, 20)
                .WithMessage("Password doesn't match");
        }
    }
}
