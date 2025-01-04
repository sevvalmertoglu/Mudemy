namespace Mudemy.Core.DTOs
{
    public class UpdateUserDto
    {
        public UpdateType UpdateType { get; set; } = UpdateType.None;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }
    }
    
    public enum UpdateType
    {
        None,
        UserNameUpdate,
        EmailUpdate,
        PasswordUpdate
    }
}
