namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class FullUserName
{
    public FullUserName(string fullName)
    {
        var s = fullName.Split(" ");

        if (s.Length > 0)
            FirstName = s[0];

        if (s.Length > 1)
            LastName = s[1];
    }

    public FullUserName()
    {

    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}