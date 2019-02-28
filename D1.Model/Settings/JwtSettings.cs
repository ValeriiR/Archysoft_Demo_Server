using System;
using System.Collections.Generic;
using System.Text;

namespace D1.Model.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }

        public int ExpireDays { get; set; }
    }
}
