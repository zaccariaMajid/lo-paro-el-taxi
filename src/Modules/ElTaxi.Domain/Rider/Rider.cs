using ElTaxi.BuildingBlocks.Domain;

namespace ElTaxi.Domain.Rider;

public class Rider : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string License { get; set; }

    public Rider(string name, string email, string license)
    {
        Name = name;
        Email = email;
        License = license;
    }
}
