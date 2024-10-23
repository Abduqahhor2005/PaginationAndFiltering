using PaginationAndFiltering.Filter;
using PaginationAndFiltering.Response;

namespace PaginationAndFiltering.Service;

public interface IStudentService
{
    PaginationResponse<IEnumerable<ReadStudent>> GetAllStudents(StudentFilter filter);
    ReadStudent? GetStudentById(int id);
    bool CreateStudent(CreateStudent student);
    bool UpdateStudent(UpdateStudent student);
    bool DeleteStudent(int id);
}