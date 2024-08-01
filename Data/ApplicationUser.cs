using System;
using weather.Components.Account.Pages.Manage;
using weather.Identity;

namespace weather.Data
{
    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();  // Assign a default value to Id
        }

        public ApplicationUser(string mailId)
            : base(RemoveGmailDomain(mailId))
        {
            Id = Guid.NewGuid().ToString();  // Assign a default value to Id
            Email = mailId;
            UserName = RemoveGmailDomain(mailId);  // Store mailId without @gmail.com as UserName
            NormalizedUserName = NormalizeUserName(UserName);  // Normalize the UserName
            NormalizedEmail = NormalizeUserName(mailId);  // Normalize the Email
        }

        private static string RemoveGmailDomain(string mailId)
        {
            return mailId.Replace("@gmail.com", "", StringComparison.OrdinalIgnoreCase);
        }

        private static string NormalizeUserName(string userName)
        {
            return userName.ToUpperInvariant();
        }
    }
}