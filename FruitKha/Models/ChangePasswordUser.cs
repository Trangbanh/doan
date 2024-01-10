namespace FruitKha.Models
{
    public class ChangePasswordUser
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
