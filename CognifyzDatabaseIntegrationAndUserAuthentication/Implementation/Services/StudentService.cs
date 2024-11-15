using CognifyzDatabaseIntegrationAndUserAuthentication.Context;
using CognifyzDatabaseIntegrationAndUserAuthentication.Dto;
using CognifyzDatabaseIntegrationAndUserAuthentication.Entities;
using CognifyzDatabaseIntegrationAndUserAuthentication.Implementation.Interface;
using CognifyzDatabaseIntegrationAndUserAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace CognifyzDatabaseIntegrationAndUserAuthentication.Implementation.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<StudentDto>> CreateStudent(StudentDto request)
        {
            var response = new ResponseModel<StudentDto>();

            try
            {
                var department = new Student
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    Course = request.Course,
                    Gender = request.Gender,
                    ParentEmail = request.ParentEmail,
                    ParentPhoneNumber = request.ParentPhoneNumber,
                    DateOfBirth = request.DateOfBirth,
                    GPA = request.GPA,
                    EnrollmentDate = DateTime.Now
                };

                _context.Students.Add(department);
                await _context.SaveChangesAsync();

                request.Id = department.Id;
                response.Success = true;
                response.Message = "Student registered successfully";
                response.Data = request;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the department";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteStudent(Guid id)
        {
            var response = new ResponseModel<bool>();

            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                response.Success = false;
                response.Message = "Student not found";
                response.Errors.Add($"No student found with ID: {id}");
                return response;
            }
            _context.Students.Remove(student);

            try
            {
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Student deleted successfully";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error deleting student";
                response.Errors.Add(ex.Message);
                response.Data = false;
            }

            return response;
        }

        public async Task<ResponseModel<StudentDto>> EditStudent(StudentDto editStudent,Guid id)
        {
            var response = new ResponseModel<StudentDto>();

            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                response.Success = false;
                response.Message = "Student not found";
                response.Errors.Add($"No student found with ID: {id}");
                return response;
            }

            student.FirstName = editStudent.FirstName;
            student.LastName = editStudent.LastName;
            student.Address = editStudent.Address;
            student.Course = editStudent.Course;
            student.ParentEmail = editStudent.ParentEmail;
            student.ParentPhoneNumber = editStudent.ParentPhoneNumber;
            student.Gender = editStudent.Gender;
            student.DateOfBirth = editStudent.DateOfBirth;
            student.GPA = editStudent.GPA;
            student.EnrollmentDate = editStudent.EnrollmentDate;

            try
            {
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Student updated successfully";
                response.Data = editStudent; 
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error updating student";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<StudentDto> GetStudentDetail(Guid id)
        {
            return await _context.Students
                .Where(x => x.Id == id)
                .Select(x => new StudentDto
                {
                    Id = x.Id,
                    Address = x.Address,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Course = x.Course,
                    ParentEmail = x.ParentEmail,
                    ParentPhoneNumber = x.ParentPhoneNumber,
                    Gender = x.Gender,
                    DateOfBirth = x.DateOfBirth,
                    GPA = x.GPA,
                    EnrollmentDate = x.EnrollmentDate

                }).FirstOrDefaultAsync();
        }

        public async Task<List<StudentDto>> GetStudentList()
        {
            return await _context.Students
                .Select(x => new StudentDto
                {
                    Id = x.Id,
                    Address = x.Address,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Course = x.Course,
                    Gender = x.Gender,
                    DateOfBirth = x.DateOfBirth,
                    GPA = x.GPA,
                    EnrollmentDate = x.EnrollmentDate
                }).ToListAsync();
        }
    }
}
