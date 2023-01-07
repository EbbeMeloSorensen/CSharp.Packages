using EntityFrameworkNet6.Domain.C2IEDM;
using Newtonsoft.Json;

Location location = new Point
{
    Id = Guid.NewGuid(),
    Dummy = 2
};

using var file1 = File.CreateText(@"C:\Temp\location.json");
var jsonSerializer1 = new JsonSerializer
{
    Formatting = Formatting.Indented
};
jsonSerializer1.Serialize(file1, location);

var locations = new List<Location>
{
    new Location
    {
        Id = Guid.NewGuid()
    },
    new Point
    {
        Id = Guid.NewGuid(),
        Dummy = 3
    },
};

using var file2 = File.CreateText(@"C:\Temp\locations.json");
var jsonSerializer2 = new JsonSerializer
{
    Formatting = Formatting.Indented
};
jsonSerializer2.Serialize(file2, locations);
