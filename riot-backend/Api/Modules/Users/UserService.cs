using riot_backend.Api.Modules.Users.Types;

namespace riot_backend.Api.Modules.Users;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Get(int id)
    {
        return _userRepository.Get(id);
    }

    public void Delete(int id)
    {
        _userRepository.Delete(id);
    }

    public void Update(int id, User user)
    {
        _userRepository.Update(id, user);
    }

    public User Insert(User user)
    {
        return _userRepository.Insert(user);
    }

    public IEnumerable<User> GetAll()
    {
        return _userRepository.GetAll();
    }
}