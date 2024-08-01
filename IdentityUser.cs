using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace weather.Identity
{
    public class IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        [PersonalData]
        public virtual TKey Id { get; set; }

        [ProtectedPersonalData]
        public virtual string? UserName { get; set; }

        public virtual string? NormalizedUserName { get; set; }

        [ProtectedPersonalData]
        public virtual string? Email { get; set; }

        public virtual string? NormalizedEmail { get; set; }

        [PersonalData]
        public virtual bool EmailConfirmed { get; set; }

        public virtual string? PasswordHash { get; set; }

        public IdentityUser()
        {
        }

        public IdentityUser(string email)
            : this()
        {
            Email = email;
            UserName = email.Replace("@gmail.com", "");
            NormalizedUserName = UserName.ToUpperInvariant();
            NormalizedEmail = email.ToUpperInvariant();
        }

        public override string ToString()
        {
            return UserName ?? string.Empty;
        }
    }
}