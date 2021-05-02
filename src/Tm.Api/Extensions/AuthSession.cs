using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tm.Api.Extensions
{
    public class AuthSession
    {
        protected readonly ClaimsPrincipal Claims;
        public AuthSession(ClaimsPrincipal claims)
        {
            Claims = claims;
        }
        public AuthSession()
        {
        }
        private string _id;

        public string Id
        {
            get
            {
                //  return Guid.Parse("6B73190F-2440-4B45-AEA8-7792C09969A8");
                if (!string.IsNullOrWhiteSpace(_id)) return _id;
                _id = Claims.FindFirst(JwtRegisteredClaimNames.Sub)?
                                     .Value ?? throw new KeyNotFoundException("Unauthorize user.");

                return _id;
            }
            set => _id = value;
        }

        private string _businessId;

        public string BusinessId
        {
            get
            {
                
                return _businessId;
            }
            set => _businessId = value;
        }


      

        private string _email;

        public string Email
        {
            get
            {
                if (_email != null) return _email;
                _email = Claims.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
                return _email;
            }
            set => _email = value;
        }

      
    }
}
