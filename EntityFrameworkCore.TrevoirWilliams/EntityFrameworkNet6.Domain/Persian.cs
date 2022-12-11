namespace EntityFrameworkNet6.Domain;

public class Persian : Cat
{
    public int Color { get; set; }

    public override string ToString()
    {
        return $"{Id} Persian, Legs: {Legs}, Color: {Color}";
    }
}