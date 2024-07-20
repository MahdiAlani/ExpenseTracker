namespace ExpenseTrackerAPI.Models
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }

        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public LoginResponseDto(UserDto user, string accessToken, string refreshToken )
        {
            User = user;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
