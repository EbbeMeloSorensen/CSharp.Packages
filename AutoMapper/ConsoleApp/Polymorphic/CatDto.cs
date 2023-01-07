namespace ConsoleApp.Polymorphic;

public class CatDto : AnimalDto
{
    public string Name { get; set; }

    public override string ToString()
    {
        return $"(CatDto) Legs: {Legs}, Name: {Name}";
    }
}