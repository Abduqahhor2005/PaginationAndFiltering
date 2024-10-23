using PaginationAndFiltering.Filter;
using PaginationAndFiltering.Response;

namespace PaginationAndFiltering.Service;

public class StudentService(DataContext context) : IStudentService
{
    public PaginationResponse<IEnumerable<ReadStudent>> GetAllStudents(StudentFilter filter)
    {
        IQueryable<Student> students = context.Students;
        if (filter.Name != null)
            students = students.Where(x => x.Name.ToLower()
                .Contains(filter.Name.ToLower()));
        if (filter.Age != null)
            students = students.Where(x => x.Age == filter.Age);
        if (filter.Email != null)
            students = students.Where(x => x.Email.ToLower()
                .Contains(filter.Email.ToLower()));
        if (filter.PhoneNumber != null)
            students = students.Where(x => x.PhoneNumber.ToLower()
                .Contains(filter.PhoneNumber.ToLower()));

        IQueryable<ReadStudent> result = students.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).Select(x =>
                new ReadStudent()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber
                });
        int totalRecords = context.Students.Count();
        return PaginationResponse<IEnumerable<ReadStudent>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public ReadStudent? GetStudentById(int id)
    {
        var user = (from u in context.Students
            where u.IsDeleted == false
            select new ReadStudent()
            {
                Id = u.Id,
                Name = u.Name,
                Age = u.Age,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).FirstOrDefault(x=>x.Id==id);
        return user;
    }

    public bool CreateStudent(CreateStudent student)
    {
        bool existUser = context.Students.Any(x =>
            x.Name.ToLower() == student.Name.ToLower() && x.IsDeleted == false);
        if (existUser) return false;
        int maxId = context.Students.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
        maxId++;
        context.Students.Add(new()
        {
            Id = maxId,
            Name = student.Name,
            Age = student.Age,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber
        });
        return true;
    }

    public bool UpdateStudent(UpdateStudent student)
    {
        Student? existingStudent = context.Students.FirstOrDefault(x => x.IsDeleted == false && x.Id == student.Id);
        if (existingStudent is null) return false;

        existingStudent.Name = student.Name;
        existingStudent.Age = student.Age;
        existingStudent.Email = student.Email;
        existingStudent.PhoneNumber = student.PhoneNumber;
        existingStudent.UpdatedAt = DateTime.UtcNow;
        return true;
    }

    public bool DeleteStudent(int id)
    {
        Student? existingStudent = context.Students.FirstOrDefault(x => x.Id == id);
        if (existingStudent is null) return false;
        existingStudent.IsDeleted = true;
        existingStudent.DeletedAt = DateTime.UtcNow;
        existingStudent.UpdatedAt = DateTime.UtcNow;
        return true;
    }
}