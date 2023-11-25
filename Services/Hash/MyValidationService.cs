using System.Text.RegularExpressions;

namespace _11112023ClassWork.Services.Hash
{
    public class MyValidationService : IValidationService
    {
        public bool IsNameValid(string name)
        {
            return !string.IsNullOrEmpty(name) && name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        public bool IsPhoneValid(string phone)
        {
            return !string.IsNullOrEmpty(phone) && Regex.IsMatch(phone, @"^\+?[0-9](?: ?[0-9]){5,13}$");
        }

        public bool IsEmailValid(string email)
        {
            return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
        }
    }
}
