using CognifyzDatabaseIntegrationAndUserAuthentication.Dto;
using CognifyzDatabaseIntegrationAndUserAuthentication.Entities;
using CognifyzDatabaseIntegrationAndUserAuthentication.Models;

namespace CognifyzDatabaseIntegrationAndUserAuthentication.Implementation.Interface
{
    public interface  IStudentService
    {
        Task<List<StudentDto>> GetStudentList();
        Task<StudentDto> GetStudentDetail(Guid id);
        Task<ResponseModel<StudentDto>> CreateStudent(StudentDto creatStudent);
        Task<ResponseModel<StudentDto>> EditStudent(StudentDto editStudent, Guid id);
        Task<ResponseModel<bool>> DeleteStudent(Guid Id);
    }
}
