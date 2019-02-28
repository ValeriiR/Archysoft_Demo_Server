using D1.Model.Services.Abstract;
using D1.Model.Settings;

namespace D1.Model.Services.Concrete
{
    public class SettingsService : ISettingsService
    {
        public JwtSettings JwtSettings { get; set; }
        public  EmailSettings EmailSettings { get; set; }
        public UIUrlSettings UiUrlSettings { get; set; }

        public SettingsService(JwtSettings jwtSettings,EmailSettings emailSettings, UIUrlSettings uiUrlSettings)
        {
            JwtSettings = jwtSettings;
            EmailSettings = emailSettings;
            UiUrlSettings = uiUrlSettings;
        }
    }
}
