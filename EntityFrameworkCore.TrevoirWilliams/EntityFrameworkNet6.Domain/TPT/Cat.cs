namespace EntityFrameworkNet6.Domain.TPT;

public class Cat
{
    public int Id { get; set; }
    public int Legs { get; set; }

    public override string ToString()
    {
        return $"{Id} Cat,     Legs: {Legs}";
    }
}