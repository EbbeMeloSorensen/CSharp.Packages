namespace EntityFrameworkNet6.Domain
{
    public class Dog
    {
        public int Id { get; set; }
        public int Legs { get; set; }

        public override string ToString()
        {
            return $"{Id} Dog,     Legs: {Legs}";
        }
    }
}
