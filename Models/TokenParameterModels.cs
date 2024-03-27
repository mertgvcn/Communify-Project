using CommunifyLibrary.Models;

namespace Communify_Backend.Models
{
    public class TokenParameterModels
    {
        public class GenerateTokenRequest
        {
            public string UserID { get; set; }
            public Role Role { get; set; }
        }

        public class GenerateTokenResponse
        {
            public string Token { get; set; }
            public DateTime TokenExpireDate { get; set; }
        }
    }
}
