using System.Collections.Concurrent;

namespace SignalRHW.Services;

public class ActiveUsersService
{
    private readonly object _lock = new object();
    private readonly List<string> _users = new();

    public void AddUser(string userName)
    {
        lock (_lock)
        {
            _users.Add(userName);
        }
    }

    public void RemoveUser(string userName)
    {
        lock (_lock)
        {
            _users.Remove(userName);
        }
    }

    public int GetUserCount()
    {
        return _users.Count;
    }
}