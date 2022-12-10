namespace EntityFrameworkNet6.Domain;

public class Team : BaseDomainObject
{
    public string Name { get; set; }

    // Kombinationen af disse 2 angiver, at det er en foreign key
    // Man kunne have anført int som nullable ved at placere et spørgsmålstegn efter - så
    // ville den være blevet en optional foreign key

    // Team har en one-to-many relation med league, hvor team er child

    // Team har en one-to-one relation med coach, hvor coach er child

    public int LeagueId { get; set; }
    public virtual League League { get; set; }
    public virtual Coach Coach { get; set; }

    public virtual List<Match> HomeMatches{ get; set; }

    public virtual List<Match> AwayMatches { get; set; }
}