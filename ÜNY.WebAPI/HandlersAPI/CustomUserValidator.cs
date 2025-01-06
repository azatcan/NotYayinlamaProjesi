using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using ÜNY.Domain.Entities;

namespace ÜNY.WebAPI.HandlersAPI
{
    public class CustomUserValidator : IUserValidator<Users>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<Users> manager, Users user)
        {
            var errors = new List<IdentityError>();

            if (string.IsNullOrEmpty(user.UserName))
            {
                errors.Add(new IdentityError
                {
                    Code = "EmptyUserName",
                    Description = "Username cannot be empty."
                });
            }

            if (!Regex.IsMatch(user.UserName, @"^\d{11}$"))
            {
                errors.Add(new IdentityError
                {
                    Code = "InvalidUserName",
                    Description = "Username must be exactly 11 digits."
                });
            }

            return Task.FromResult(errors.Any() ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success);
        }
    }
}
