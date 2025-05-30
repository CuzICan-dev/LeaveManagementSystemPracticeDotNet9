using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystemPractice.web.Data.Entities;

namespace LeaveManagementSystemPractice.web.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<LeaveType> LeaveType { get; set; } = default!;

public DbSet<Period> Period { get; set; } = default!;
}