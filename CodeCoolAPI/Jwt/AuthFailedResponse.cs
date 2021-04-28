using System.Collections.Generic;

namespace CodeCoolAPI.Jwt
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}