using Microsoft.AspNetCore.Mvc;
using PaginationAndFiltering.Filter;
using PaginationAndFiltering.Response;
using PaginationAndFiltering.Service;

namespace PaginationAndFiltering;

[ApiController]
[Route("api/user")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCourses([FromQuery] StudentFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<ReadStudent>>>.Success(null,
            studentService.GetAllStudents(filter)));
    [HttpGet("{id:int}")]
    public IActionResult GetStudentById(int id)
    {
        ReadStudent? res = studentService.GetStudentById(id);
        return res != null
            ? Ok(ApiResponse<ReadStudent?>.Success(null, res))
            : NotFound(ApiResponse<ReadStudent?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateStudent([FromBody] string name,int age, string email, string phoneNumber)
    {
        CreateStudent student = new CreateStudent(name, age, email,phoneNumber);
        bool res = studentService.CreateStudent(student);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateCourse(UpdateStudent student)
    {
        bool res = studentService.UpdateStudent(student);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteStudent(int id)
    {
        bool res = studentService.DeleteStudent(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}