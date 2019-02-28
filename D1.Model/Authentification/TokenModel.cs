using System;


namespace D1.Model.Authentification
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
