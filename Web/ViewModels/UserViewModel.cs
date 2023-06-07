using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.ViewModels
{
    public class UserViewModel: IViewModel
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public UserRole Role { get; set; }
        [DataType(DataType.Upload), FromForm(Name = "Image")]
        public IFormFile? Image { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public UserViewModel()
        {
            Id = Guid.Empty;
            BranchId = Guid.Empty;
            FirstName = string.Empty;
            UserName = string.Empty;
            LastName = string.Empty;
            Password = string.Empty;
            Role = UserRole.Guest;
            Image = default;
            Email = string.Empty;
        }
        public UserViewModel(
            Guid id,
            Guid branchId,
            string userName,
            string password,
            UserRole role,
            IFormFile? image,
            string email
        )
        {
            Id = id;
            BranchId = branchId;
            UserName = userName;
            if (userName.Contains(" "))
            {
                FirstName = userName.Split(" ")[0];
                LastName = userName.Split(" ")[1];
            } else
            {
                FirstName = string.Empty;
                LastName = string.Empty;
            }
            Password = password;
            Role = role;
            Image = image;
            Email = email;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}