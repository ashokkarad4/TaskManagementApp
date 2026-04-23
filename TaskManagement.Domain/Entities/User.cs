using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
        
        [StringLength(255)]
        public string? Address { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public DateTime Created { get; set; } = DateTime.Now;   
        public DateTime LastUpdated { get; set; }
        
        public ICollection<TaskItem> Tasks { get; set; }
        
        // Computed property for full name
        public string FullName => $"{FirstName} {LastName}";
    }
}
