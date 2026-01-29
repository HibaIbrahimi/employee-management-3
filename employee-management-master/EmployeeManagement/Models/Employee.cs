using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Adress")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Length(8, 20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }
    }
}
