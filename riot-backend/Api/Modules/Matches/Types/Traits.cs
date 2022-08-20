namespace riot_backend.Api.Modules.Matches.Types;

public class Traits
{
    /**
     * Trait name.
     */
    public string name { get; set; }

    /**
     * Number of units with this trait.
     */
    public int numUnits { get; set; }

    /**
     * Current style for this trait. (0 = No style, 1 = Bronze, 2 = Silver, 3 = Gold, 4 = Chromatic)
     */
    public int style { get; set; }

    /**
     * Current active tier for the trait.
     */
    public int tierCurrent { get; set; }

    /**
     * Total tiers for the trait.
     */
    public int tierTotal { get; set; }

    public static Traits FromJson(dynamic json)
    {
        return new Traits
        {
            name = json.name,
            numUnits = json.num_units,
            style = json.style,
            tierCurrent = json.tier_current,
            tierTotal = json.tier_total
        };
    }
}