using GraphQLPractise.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLPractise.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> User { get; set; }
	}
}

