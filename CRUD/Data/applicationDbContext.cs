using CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Data
{
	public class applicationDbContext : DbContext
	{
		public applicationDbContext(DbContextOptions<applicationDbContext> options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
	}
}
