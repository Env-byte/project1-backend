using riot_backend.Api.Modules.Traits.Types;

namespace riot_backend.Api.Modules.Traits;

public class TraitService
{
    private readonly TraitProvider _traitProvider;

    public TraitService(TraitProvider traitProvider)
    {
        _traitProvider = traitProvider;
    }

    public List<Trait> GetAll()
    {
        return _traitProvider.GetAll();
    }

    public Trait Get(string name)
    {
        return _traitProvider.Get(name);
    }
}