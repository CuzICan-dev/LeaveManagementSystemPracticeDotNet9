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

public DbSet<LeaveManagementSystemPractice.web.Data.Entities.LeaveType> LeaveType { get; set; } = default!;
}