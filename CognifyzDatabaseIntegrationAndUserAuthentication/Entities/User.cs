using Microsoft.AspNetCore.Identity;

namespace CognifyzDatabaseIntegrationAndUserAuthentication.Entities
{
    public class User : IdentityUser
    {
        public UserRole UserRole { get; set; }
        public string Address { get; set; }
        public required string FullName { get; set; }
    }
}


