using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystemPractice.web.Data.Entities;

public class LeaveType
{
    public int Id { get; set; }
    
    [StringLength(150)] // Limits the string length to 150 characters
    public string Name { get; set; }
    
    public int NumberOfDays { get; set; }
}