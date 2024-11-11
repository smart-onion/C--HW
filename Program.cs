using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var configurationService = app.Services.GetService<IConfiguration>();
string connectionString = configurationService["ConnectionStrings:DefaultConnection"];

int pageSize = 5;

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    response.ContentType = "text/html; charset=utf-8";

    //При переходе на главную страницу, считываем всех пользователей
    if (request.Path == "/")
    {
        var query = request.Query;
        if (!query.Any()) response.Redirect("/?page=1");
        int pageNumber = 1;
        int.TryParse(query?["page"], out pageNumber);
        string querySql = @$"
            SELECT Id, Name, Age
            FROM Users
            ORDER BY Id
            OFFSET @PageSize * (@PageNumber - 1) ROWS
            FETCH NEXT @PageSize ROWS ONLY;";

        List<User> users = new List<User>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(querySql, connection);
            command.Parameters.AddWithValue("@PageNumber", pageNumber);
            command.Parameters.AddWithValue("@PageSize", pageSize);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
            }
        }
        await response.WriteAsync(GenerateHtmlPage(BuildHtmlTable(users), "All Users from DataBase"));
    }
    else if (request.Path == "/adduser" && request.Method == HttpMethods.Get)
    {
        await response.WriteAsync(GenerateHtmlPage(BuildAddUserForm(), "All Users from DataBase"));
    }
    else if (request.Path == "/adduser" && request.Method == HttpMethods.Post)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var form = request.Form;
            string name = form["name"];
            string age = form["age"];

            string query = $"INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Age", age);
            await command.ExecuteNonQueryAsync();
            response.Redirect("/");
        }
    }
    else if (request.Path == "/edituser" && request.Method == HttpMethods.Get)
    {
        var quey = request.Query;
        var id = quey["id"];
        await response.WriteAsync(GenerateHtmlPage(BuildEditUserFrom(id), "All Users from DataBase"));
    }
    else if (request.Path == "/edituser" && request.Method == HttpMethods.Post) 
    { 
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var form = request.Form;
            string id = form["id"];
            string name = form["name"];
            string age = form["age"];

            string sqlQuery = "UPDATE Users SET Name = @Name, Age = @Age WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Age", age);
            await command.ExecuteNonQueryAsync();
            response.Redirect("/");
        }

    }
    else if (request.Path == "/deleteuser" && request.Method == HttpMethods.Post)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var form = request.Form;
            string id = form["id"].ToString();

            string sqlQuery = "DELETE FROM Users WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync();
            response.Redirect("/");
        }
    }
    else if (request.Path == "/finduser" && request.Method == HttpMethods.Post)
    {
        var form = request.Form;
        var name = form["name"].ToString();
        var query = request.Query;
        int pageNumber = 1;
        int.TryParse(query?["page"], out pageNumber);

        List<User> users = new List<User>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand("select Id, Name, Age from Users WHERE Name = @Name", connection);
            command.Parameters.AddWithValue("@Name", name);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
            }
        }
        string message = $"All '{name}' Users from DataBase";
        if (!users.Any()) message = $"User: {name} not found!";
        
        await response.WriteAsync(GenerateHtmlPage(BuildHtmlTable(users), message));
    }
    else if (request.Path == "/sortusers" && request.Method == HttpMethods.Get)
    {
        var query = request.Query;
        var sortCriteria = query["sortCriteria"].ToString();
        int pageNumber = 1;
        int.TryParse(query?["page"], out pageNumber);
        List<User> users = new List<User>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand($"SELECT Id, Name, Age FROM Users ORDER BY {sortCriteria}", connection);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
            }
        }
        await response.WriteAsync(GenerateHtmlPage(BuildHtmlTable(users), "All Users from DataBase"));
    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync("Page Not Found");
    }
});

app.Run();

static string BuildAddUserForm()
{
    StringBuilder userForm = new StringBuilder();
    userForm.Append("<form action=\"/adduser\" method=\"post\">" +
        "<p>User Name: </p><input type=\"text\" name=\"name\" value=\"\" /><p>Age: </p>" +
        "<input type=\"number\" name=\"age\" value=\"18\" />" +
        "<button type=\"submit\" class=\"btn btn-primary\" id=\"\">Add</button>" +
        "</form>");
    return userForm.ToString();
}
static string BuildEditUserFrom(string id)
{
    StringBuilder userForm = new StringBuilder();
    userForm.Append("<form action=\"/edituser\" method=\"post\">" +
        $"<input type=\"hidden\" name=\"id\" value=\"{id}\" />" +
        "<p>User Name: </p><input type=\"text\" name=\"name\" value=\"\" /><p>Age: </p>" +
        "<input type=\"number\" name=\"age\" value=\"18\" />" +
        "<button type=\"submit\" class=\"btn btn-primary\" id=\"\">Edit</button>" +
        "</form>");
    return userForm.ToString();
}
static string BuildHtmlTable<T>(IEnumerable<T> collection)
{
    StringBuilder tableHtml = new StringBuilder();
    tableHtml.Append($"""
            <form class="d-flex" action="/finduser" method="post" >
            <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search" name="name" required>
            <button class="btn btn-outline-success" type="submit">Search</button>
        </form>
        """);
    tableHtml.Append("""
               <form method="get" action="/sortusers">
            <select name="sortCriteria" onchange="this.form.submit()">
                <option value="" disabled selected>Select sorting option</option>
                <option value="Name">Sort by Name</option>
                <option value="Age">Sort by Age</option>
            </select>
        </form>
        </form>
        """);
    tableHtml.Append("<form action=\"/adduser\" method=\"get\"><button type=\"submit\" class=\"btn btn-primary\">Add user</button></form>");
    tableHtml.Append("<table class=\"w-50\">");

    PropertyInfo[] properties = typeof(T).GetProperties();

    tableHtml.Append("<tr>");
    foreach (PropertyInfo property in properties)
    {
        tableHtml.Append($"<th class=\"mt-3 mb-2\">{property.Name}</th>");
    }
    tableHtml.Append("<th>Action</th>");
    tableHtml.Append("</tr>");

    foreach (T item in collection)
    {
        tableHtml.Append("<tr>");
        int id = -1;
        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(item);
            if (property.Name == "Id") id = (int)value;
            tableHtml.Append($"<td>{value}</td>");
            
        }
        tableHtml.Append($"<td><a href=\"edituser?id={id}\"><button class=\"btn btn-primary\">Edit</button></a></td>");
        tableHtml.Append($"<td><form action=\"/deleteuser\" method=\"post\">" +
            $"<input type=\"hidden\" name=\"id\" value=\"{id}\" />" +
            $"<button class=\"btn btn-danger\">Delete</button>" +
            $"</form></td>");
        tableHtml.Append("</tr>");
    }

    tableHtml.Append("</table>");
    tableHtml.Append($$$"""
        <nav aria-label="Page navigation example">
            <ul class="pagination">
                <li class="page-item">
                    <a id="prevPage"><button class="page-link" >Previous</button></a>
                </li>
                <li class="page-item">
                    <a id="nextPage"><button class="page-link">Next</button></a>
                </li>
            </ul>
        </nav>
        <script>
            function getQueryParams() {
                const params = new URLSearchParams(window.location.search);
                const page = params.get('page');
                return page;
            }

            function changePagination() {
                let page = getQueryParams();
                let prevPage = page - 1;
                let nextPage = Number(page) + 1;
                prevPage <= 0 ? prevPage = 1 : prevPage;
                document.getElementById("prevPage").setAttribute("href", `/?page=${prevPage}`)
                document.getElementById("nextPage").setAttribute("href", `/?page=${nextPage}`)
            }
            changePagination()
        </script>
        """);
    return tableHtml.ToString();
}

static string GenerateHtmlPage(string body, string header)
{
    string html = $"""
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="utf-8" />
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" 
            integrity="sha384-KK94CHFLLe+nY2dmCWGMq91rCGa5gtU4mk92HdvYe+M/SXH301p5ILy+dN9+nJOZ" crossorigin="anonymous">
            <title>{header}</title>
        </head>
        <body>
        <div class="container">
        <h2 class="d-flex justify-content-center">{header}</h2>
        <div class="mt-5"></div>
        {body}
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
        </div>
        </body>
        </html>
        """;
    return html;
}

record User(int Id, string Name, int Age)
{
    public User(string Name, int Age) : this(0, Name, Age) { }
}
