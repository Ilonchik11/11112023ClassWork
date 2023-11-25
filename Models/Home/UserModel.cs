using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _11112023ClassWork.Models.Home
{
    public class UserModel
    {
        [FromForm(Name = "user-name")]
        public string Name { get; set; } = null!;
        [FromForm(Name = "user-phone")]
        public string Phone { get; set; } = null!;
        [FromForm(Name = "user-email")]
        public string Email { get; set; } = null!;
        public ValidationResultModel? ValidationResult { get; set; }

    }
}
