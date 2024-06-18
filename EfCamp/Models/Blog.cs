namespace EfCamp;

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Blog
{
    public int BlogId { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public List<Blog> GetAll()
    {
        List<Blog> blogs = [];

        using var db = new BloggingContext();

        try
        {
            blogs = db.Blogs.ToList();
        }
        catch (Exception ex)
        {
            //
            Console.WriteLine($"Error while fetching blogs: {ex.Message}");
        }

        return blogs;
    }

    public void Add(string name, string url)
    {
        using var db = new BloggingContext();

        try
        {
            var newBlog = new Blog
            {
                Name = name,
                Url = url
            };

            db.Blogs.Add(newBlog);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while adding blogs: {ex.Message}");
        }
    }
}
