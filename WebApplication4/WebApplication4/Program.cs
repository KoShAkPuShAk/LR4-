using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("config.json");

Library library = builder.Configuration.GetSection("Library").Get<Library>() ?? new Library();

var app = builder.Build();
Profile myProfile = new Profile();
myProfile.Name = "Name";
myProfile.Surname = "Surname";
app.MapGet("/library", () => "Hello World!");
app.MapGet("/library/books", () => { 
    StringBuilder stringBuilder = new StringBuilder();
    foreach(var book in library.books){
        stringBuilder.Append($"Title:{book.Title}\nAuthor:{book.Author}\n");
    }
    return stringBuilder.ToString();
});
app.MapGet("/library/profile/{id?}", (int? id) =>
{
    Profile profile;
    if (id is null)
    {
        profile = myProfile;
    }
    else
    {
        profile = library.profiles[(int)id];
    }
    return $"profile name:{profile.Name}\nProfile surname:{profile.Surname}";
    



});

app.Run();
