namespace PaginationAndFiltering;

public readonly record struct ReadStudent(int Id, string Name,int Age, string Email, string PhoneNumber);

public readonly record struct CreateStudent(string Name,int Age, string Email, string PhoneNumber);

public readonly record struct UpdateStudent(int Id,string Name,int Age, string Email, string PhoneNumber);