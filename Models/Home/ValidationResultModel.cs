namespace _11112023ClassWork.Models.Home
{
    public class ValidationResultModel
    {
        public bool IsNameValid { get; set; }
        public bool IsPhoneValid { get; set; }
        public bool IsEmailValid { get; set; }
        public UserModel? FormModel { get; set; }
    }
}
