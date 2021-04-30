using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public class AppSettings
    {
        public string SigningKey { get; set; }
        public string Environment { get; set; }
        public Tokens Tokens { get; set; }
    }
    public class Tokens
    {
        public string Key { get; set; }
        public long Lifetime { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
