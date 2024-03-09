// Program.cs är ditt program, där saker händer
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;



BlogContext db = new();

db.Blogs.Add(new Blog { Name = "Aftonbladet" });
//db.SaveChanges();
Blog blog = db.Blogs.Where(b => b.Name == "Aftonbladet").First();

db.users.Add(new User { Username = "Dwayne Johnson" });

db.Categories.Add(new Category { CategoryName = "Football" });

db.users.First().Articles.Add(new Article { Title = "Victory for man united", Blog = blog });
db.SaveChanges();

db.Categories.First().Articles.Add(db.users.First().Articles.Where(a => a.Title == "Victory for man united").First());
db.SaveChanges();


Console.WriteLine(value: db.Categories.First().Articles
    .First().User.Articles
    .First().Blog.Articles
    .First().Title);



public class BlogContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Blog> Blogs { get; set; }


    string _path = "blogging.db";

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={_path}");
}


public class Blog
{
    public int BlogID { get; set; }
    public string? Name { get; set; }
    public List<Article> Articles { get; } = new();
}

public class Article
{
    public int ArticleID { get; set; }
    public string? Title { get; set; }
    public List<Category> Categories { get; } = new();
    public Blog? Blog { get; set; }
    public User? User { get; set; }
}

public class Category
{
    public int CategoryID { get; set; }
    public string? CategoryName { get; set; }
    
    public List<Article> Articles { get; } = new();
}

public class User
{
    public int UserID { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public List<Article> Articles { get; } = new();


}

