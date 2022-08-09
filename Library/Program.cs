using Library.Abstractions;
using Library.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IReader<Book>>(reader => new Library.Services.JsonBookReader(builder.Configuration["LibraryJSOMPath"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.MapGet("/api/library/getbytitle/{title}", (string title, IReader<Book> reader) =>
{
    var books = reader.ReadData();

    return books.Where(x => x.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
})
.WithName("GetByTitle");

app.MapGet("/api/library/getbyauthor/{author}", (string author, IReader<Book> reader) =>
{
    var books = reader.ReadData();

    return books.Where(x => x.Authors.Any(x => x.Name.Equals(author, StringComparison.InvariantCultureIgnoreCase))).ToList();
})
.WithName("GetByAuthor");

app.MapGet("/api/library/getbycategory/{category}", (string category, IReader<Book> reader) =>
{
    var books = reader.ReadData();

    return books.Where(x => x.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase)).ToList();
})
.WithName("GetByCategory");

app.Run();