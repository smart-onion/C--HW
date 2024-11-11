var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.Run(async (context) =>
{
    var responce = context.Response;
    var request = context.Request;
    responce.ContentType = "text/html;charset=utf8";
    if (request.Path == "/")
    {
        await responce.SendFileAsync("wwwroot/index.html");
    }
    else if (request.Path == "/upload/" && request.Method == HttpMethods.Get)
    {
        await responce.SendFileAsync("wwwroot/upload.html");
    }
    else if (request.Path == "/upload" && request.Method == HttpMethods.Post)
    {
        IFormFileCollection files = request.Form.Files;
        var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

        foreach (var file in files)
        {
            string fullPath = Path.Combine(uploadPath, file.FileName);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        responce.Redirect("/download");
    }
    else if (request.Path == "/download")
    {
        await responce.SendFileAsync("wwwroot/download.html");
    }
    else if (request.Path == "/files" && request.Method == HttpMethods.Get)
    {
        List<string> files = new();
        var a = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "uploads"));
        foreach (var file in a)
        {
            files.Add(Path.GetFileName(file));
        }
        await responce.WriteAsJsonAsync(files);
    }
    else
    {
        responce.StatusCode = 404;
        await responce.WriteAsync("Not found");
    }

});

app.Run();
