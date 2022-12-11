namespace EntityFrameworkNet6.Domain.Trevor;

public class League : BaseDomainObject
{
    public string Name { get; set; }

    // Man kan have denne med, vistnok for at facilitere navigation fra en Parent entitet til en
    // child entitet, men man behøver ikke at have den med. Bemærk, hvordan han laver den som en liste -
    // jeg er også stødt på den udkommenterede version som en god måde at gøre det på
    public List<Team> Teams { get; set; }
    //public virtual ICollection<Team>? Teams { get; set; }
}