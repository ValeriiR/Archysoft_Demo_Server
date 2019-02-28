using D1.Model.Settings;

namespace D1.Model.Services.Abstract
{
    public interface ISettingsService
    {
        JwtSettings JwtSettings { get; set; }
        EmailSettings EmailSettings { get; set; }
        UIUrlSettings UiUrlSettings { get; set; }

    }
}
