using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using VotingSystem.API.Data;
using VotingSystem.API.Models;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
// DbContext
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Максимальное количество повторных попыток
            maxRetryDelay: TimeSpan.FromSeconds(30), // Максимальное время ожидания между попытками
            errorCodesToAdd: null // null или пустой список - использует стандартные ошибки для повтора
        );
    });
});
 
// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration["Redis:Configuration"]));
 
var app = builder.Build();
 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.Migrate();
 
    if (!db.Polls.Any())
    {
        var poll1 = new Poll
        {
            Title = "Favorite programming language",
            Questions = new List<Question>
        {
            new Question
            {
                Text = "What language do you use most often?",
                Options = new List<AnswerOption>
                {
                    new AnswerOption { Text = "C#" },
                    new AnswerOption { Text = "JavaScript" },
                    new AnswerOption { Text = "Python" }
                }
            },
            new Question
            {
                Text = "What language do you see as promising?",
                Options = new List<AnswerOption>
                {
                    new AnswerOption { Text = "Rust" },
                    new AnswerOption { Text = "Go" },
                    new AnswerOption { Text = "TypeScript" }
                }
            }
        }
        };
 
        var poll2 = new Poll
        {
            Title = "Daily tech habits",
            Questions = new List<Question>
        {
            new Question
            {
                Text = "How often do you check tech news?",
                Options = new List<AnswerOption>
                {
                    new AnswerOption { Text = "Multiple times a day" },
                    new AnswerOption { Text = "Once a day" },
                    new AnswerOption { Text = "Rarely" }
                }
            },
            new Question
            {
                Text = "Do you listen to tech podcasts?",
                Options = new List<AnswerOption>
                {
                    new AnswerOption { Text = "Yes, regularly" },
                    new AnswerOption { Text = "Sometimes" },
                    new AnswerOption { Text = "Never" }
                }
            },
            new Question
            {
                Text = "How do you usually learn new technologies?",
                Options = new List<AnswerOption>
                {
                    new AnswerOption { Text = "YouTube" },
                    new AnswerOption { Text = "Online courses" },
                    new AnswerOption { Text = "Books and blogs" }
                }
            }
        }
        };
 
        db.Polls.AddRange(poll1, poll2);
        db.SaveChanges();
    }
}
 
// Middleware
app.UseSwagger();
app.UseSwaggerUI();
 
// app.UseHttpsRedirection(); // отключено для Docker (HTTP only)
 
app.MapControllers();
app.Run();