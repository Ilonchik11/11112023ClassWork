namespace _11112023ClassWork.Services.Hash
{
    public interface IValidationService
    {
        bool IsNameValid(string name);
        bool IsPhoneValid(string phone);
        bool IsEmailValid(string email);
    }
}
