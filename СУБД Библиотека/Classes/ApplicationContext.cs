using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;

namespace СУБД_Библиотека.Classes
{
    public class Rent
    {
        [Key]
        public int ID { get; set; }
        public int? Reader { get; set; }
        public string? Date_of_taking { get; set; }
        public string? Return_date { get; set; }
        public int? Publication { get; set; }
    }

    public class Rentss
    {
        [Key]
        public int ID { get; set; }
        public string? Reader { get; set; }
        public string? Date_of_taking { get; set; }
        public string? Return_date { get; set; }
        public string? Publication { get; set; }
    }

    public class Author
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
    }

    public class User
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public int Status { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int RoleID { get; set; }

        public static User currentUser;
    }

    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
    }

    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Years { get; set; }
        public string? Genre { get; set; }
        public bool Actual { get; set; }
        public byte[]? Image { get; set; }

        public string ActualText
        {
            get
            {
                return (Actual) ? "В наличии" : "Занята";
            }
        }
    }

    public class Userss
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Phone { get; set; }
        public string? Status { get; set; }
        public string? RoleID { get; set; }
    }

    public class Publication
    {
        [Key]
        public int ID { get; set; }
        public int? Author { get; set; }
        public string? Title { get; set; }
        public string? Years { get; set; }
        public int? Genre { get; set; }
        public bool Actual { get; set; }
        public byte[]? Image { get; set; }

        public string ActualText
        {
            get
            {
                return (Actual) ? "В наличии" : "Занята";
            }
        }
    }

    public class Genre
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
    }
    
    public class ApplicationContext : DbContext
    {
        public static ApplicationContext _context;
        public static ApplicationContext Getcontext()
        {
            if (_context == null)
                _context = new ApplicationContext();
            return _context;
        }
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Publication> Publication { get; set; } = null!;
        public DbSet<Author> Author { get; set; } = null!;
        public DbSet<Rent> Rent { get; set; } = null!;
        public DbSet<Genre> Genre { get; set; } = null!;
        public DbSet<Book> Book { get; set; } = null!;
        public DbSet<Role> Role { get; set; } = null!;
        public DbSet<Userss> Userss { get; set; } = null!;
        public DbSet<Rentss> Rentss { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=185.26.122.80; user=host1849318_koryakina; password=qwerty123!; database=host1849318_koryakina;",
                new MySqlServerVersion(new Version(5, 7, 39)));
        }
    }
}
