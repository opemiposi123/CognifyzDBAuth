using CognifyzDatabaseIntegrationAndUserAuthentication.Dto;
using CognifyzDatabaseIntegrationAndUserAuthentication.Implementation.Interface;
using CognifyzDatabaseIntegrationAndUserAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CognifyzDatabaseIntegrationAndUserAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<ResponseModel<StudentDto>>> CreateStudent([FromBody] StudentDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _studentService.CreateStudent(request);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<ResponseModel<bool>>> DeleteStudent(Guid id)
        {
            var response = await _studentService.DeleteStudent(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut("edit-student")]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<ResponseModel<StudentDto>>> EditStudent([FromBody] StudentDto editStudent, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _studentService.EditStudent(editStudent,id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentDetail(Guid id)
        {
            var student = await _studentService.GetStudentDetail(id);
            if (student == null)
            {
                return NotFound(new { Message = "Student not found" });
            }
            return Ok(student);
        }

        [HttpGet("all-student")]
        public async Task<ActionResult<List<StudentDto>>> GetStudentList()
        {
            var students = await _studentService.GetStudentList();
            return Ok(students);
        }
    }

}
