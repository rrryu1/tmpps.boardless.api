using System.Globalization;

namespace Tmpps.Boardless.UseCases.Account
{
    public class SignUpArgs
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}