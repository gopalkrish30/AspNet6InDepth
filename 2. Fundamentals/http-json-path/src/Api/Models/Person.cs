namespace Api.Models;

public class Person
{
    public Guid id { get; set; }

    public string Title { get; set; }   

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Gender { get; set; }

    public string Company { get; set; }

    public string Designation { get; set; }
}
