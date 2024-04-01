using CommunifyLibrary.Models;

namespace Communify_Backend.Models
{
    public class AuthenticationModels
    {
        public class UserLoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UserLoginResponse
        {
            public bool AuthenticateResult { get; set; }
            public string AuthToken { get; set; }
            public DateTime AccessTokenExpireDate { get; set; }
            public string ReplyMessage { get; set; }
            public string Role { get; set; }

        }

        public class UserRegisterRequest
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string PhoneNumber { get; set; }

            public string Email { get; set; }

            public DateTime BirthDate { get; set; }

            public Genders Gender { get; set; }

            public string BirthCountry { get; set; }

            public string BirthCity { get; set; }

            public string CurrentCountry { get; set; }

            public string CurrentCity { get; set; }

            public string Address { get; set; }

            public int[] InterestIdList { get; set; }
        }

        public class UserRegisterResponse
        {
            public bool isSuccess { get; set; }
            public string Token { get; set; }
            public DateTime TokenExpireDate { get; set; }
        }

        public class isEmailAvailableRequest
        {
            public string Email { get; set; }
        }

        public class SetPasswordRequest
        {
            public string Password { get; set; }
        }

    }
}
