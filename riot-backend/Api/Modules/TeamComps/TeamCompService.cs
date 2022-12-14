using riot_backend.Api.Modules.TeamComps.Models;
using riot_backend.Api.Modules.TeamComps.Types;
using riot_backend.ScopedTypes;

namespace riot_backend.Api.Modules.TeamComps;

public class TeamCompService
{
    private readonly TeamCompRepository _repository;
    private readonly Header _header;

    public TeamCompService(TeamCompRepository teamCompRepository, Header header)
    {
        _repository = teamCompRepository;
        _header = header;
    }

    public string Create(TeamRequest teamRequest)
    {
        if (_header.User == null)
        {
            throw new AccessDeniedException("You must be logged in");
        }
        return _repository.Insert(teamRequest);
    }

    public Team Get(string guuid)
    {
        var team = _repository.Get(guuid);
        if (team.IsPublic)
        {
            if (_header.User == null || team.CreatedBy != _header.User.id)
            {
                team.IsReadonly = true;
            }
        }
        else
        {
            if (_header.User == null || team.CreatedBy != _header.User.id)
            {
                throw new AccessDeniedException("You do not have access to this private team");
            }
        }

        return team;
    }

    public List<Team> GetPublic()
    {
        return _repository.List();
    }

    public bool UpdateOptions(string guuid, OptionsRequest optionsRequest)
    {
        //check we have write access to this team
        var team = Get(guuid);
        if (team.IsReadonly)
        {
            throw new AccessDeniedException("Trying to update a team you did not create");
        }
        _repository.UpdateOptions(guuid, optionsRequest);
        return true;
    }

    public bool Update(string guuid, TeamRequest teamRequest)
    {
        //check we have write access to this team
        var team = Get(guuid);
        if (team.IsReadonly)
        {
            throw new AccessDeniedException("Trying to update a team you did not create");
        }
        _repository.Update(team.Id, teamRequest);
        return true;
    }

    public List<Team> ListUser()
    {
        if (_header.User == null)
        {
            throw new AccessDeniedException("You must be logged in");
        }
        return _repository.ListUserTeams(_header.User.id);
    }
}