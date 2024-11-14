using hw3.Model;
using System.Text;

namespace hw3.Html
{
    public static class HtmlBuilder
    {
        public static string BuildHtmlTemplate(string title, string content)
        {
            var page = new StringBuilder();
            page.Append($@"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>{title}</title>
                    <!-- Bootstrap CSS -->
                    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
                    <!-- jQuery library -->
                    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js""></script>
                    <!-- Popper JS -->
                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js""></script>
                    <!-- Bootstrap JS -->
                    <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js""></script>
                </head>
                <book>
                ");
            page.Append(content);

            page.Append(@"</body>
                </html>");

            return page.ToString();
        }


        public static string AllBooksPage(List<Book> books) 
        {
            var page = @"<table>
                            <tr>
                                <th>Author</th><
                                <th>Title</th>
                            </tr>";
            foreach (Book book in books)
            {
            page += @$"<tr>
                        <td>{book.Author}</td>
                        <td>{book.Title}</td>
                    </tr>";

            }
            page += "</table>";
            return BuildHtmlTemplate("AllBooks",page);
        }
        public static string ShowBook(Book book)
        {
            return $@"
        <h1>{book.Title}</h1>
            <h3>{book.Author}</h3>
            <p>{book.Text}</p>
        ";
        }
    }

   
}
