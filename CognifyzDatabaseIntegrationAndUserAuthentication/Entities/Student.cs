namespace CognifyzDatabaseIntegrationAndUserAuthentication.Entities
{
    public class Student
    {
        public Guid Id { get; set; }               
        public string FirstName { get; set; }      
        public string LastName { get; set; }      
        public DateTime DateOfBirth { get; set; }  
        public string Gender { get; set; }         
        public string ParentEmail { get; set; }         
        public string ParentPhoneNumber { get; set; }   
        public string Address { get; set; }        
        public DateTime EnrollmentDate { get; set; }
        public string Course { get; set; }       
        public decimal GPA { get; set; }           

        public string FullName => $"{FirstName} {LastName}";
    }

}
