namespace ConsoleApp.Polymorphic;

public class AnimalDto
{
    public int Legs { get; set; }

    public override string ToString()
    {
        return $"(AnimalDto) Legs: {Legs}";
    }
}