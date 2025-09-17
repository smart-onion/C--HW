
ChatMediator chat = new ChatMediator();
 
User alice = new User(chat, "Alice");
User bob = new User(chat, "Bob");
User charlie = new User(chat, "Charlie");
 
chat.Register(alice);
chat.Register(bob);
chat.Register(charlie);
 
alice.Send("Hello, people!");
bob.Send("Hello, Alice!");


public interface IMediator
{
    void SendMessage(string message, Colleague colleague);
}
 
public abstract class Colleague
{
    protected IMediator _mediator;
 
    public Colleague(IMediator mediator)
    {
        _mediator = mediator;
    }
}

public class User : Colleague
{
    public string Name { get; }
 
    public User(IMediator mediator, string name) : base(mediator)
    {
        Name = name;
    }
 
    public void Send(string message)
    {
        Console.WriteLine($"{Name} sends a message: {message}");
        _mediator.SendMessage(message, this);
    }
 
    public void Receive(string message)
    {
        Console.WriteLine($"{Name} received a message: {message}");
    }
}

public class ChatMediator : IMediator
{
    private List<User> _users = new List<User>();
 
    public void Register(User user)
    {
        _users.Add(user);
    }
 
    public void SendMessage(string message, Colleague sender)
    {
        foreach (var user in _users)
        {
            if (user != sender)
            {
                user.Receive(message);
            }
        }
    }
}