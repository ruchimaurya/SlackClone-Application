
using SlackClone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.SignalR.Client;

namespace SlackClone.usersDB
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
           : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}
