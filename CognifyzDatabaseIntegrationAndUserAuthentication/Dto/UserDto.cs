using CognifyzDatabaseIntegrationAndUserAuthentication.Entities;

namespace CognifyzDatabaseIntegrationAndUserAuthentication.Dto
{
    public class UserDto
    {
        public string Id { get; set; }         
        public string UserName { get; set; }    
        public string Email { get; set; }        
        public string PhoneNumber { get; set; }        
        public UserRole UserRole { get; set; }   
        public string Address { get; set; }     
        public string FullName { get; set; }     
    }

}
