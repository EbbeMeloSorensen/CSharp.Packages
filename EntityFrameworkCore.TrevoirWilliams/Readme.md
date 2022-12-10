En foreign key attribut repræsenteres sædvanligvis ved 2 attributter som vist nedenfor:

```
public class League
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    //public virtual ICollection<Team>? Teams { get; set; }
}

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int LeagueId { get; set; }
    public virtual League League { get; set; }
}
```

Man kunne også have haft en collection i parent entiteten (vist udkommenteret ovenfor). Instruktøren gør det ikke i denne omgang.

Når man har lavet en migration, kan man generere et traditionelt database create script i sql ved at eksekvere følgende i Visual Studios Package Manager Console:

```
script-migration
```

Han nævner, at man også kan reverse engineere sine domæneklasser med udgangspunkt i en eksisterende database med en kommando alla denne Package Manager Console i Visual Studio (jeg fik det dog ikke lige til at virke):

```
Scaffold-DbContext -provider Microsoft.EntityFrameworkCore.SqlServer -connection "Data Source=MELO-HOME/SQLEXPRESS;User=sa;Password=L1on8Zebra;Initial Catalog=FootballLeague_EFCore"
```

Han demonstrerer, hvordan man med Visual Studio extensionen **"EF Core Power Tools"** kan lave ER-diagrammer i Visual Studio. Det lader til at være ret brugbart. Når extensionen er installeret, kan man højreklikke på et projekt med en DbContext og vælge "EF Core Power Tools" og derefter "Add DbContext diagram".