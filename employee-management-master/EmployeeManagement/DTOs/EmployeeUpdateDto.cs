using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTOs
{
    public class EmployeeUpdateDto
    {
        // فـ Update غادي ناخدو id من URL، ماشي من body
        [Required(ErrorMessage = "First name is required")]
        [StringLength(
            50,
            ErrorMessage = "First name must be between 1 and 50 characters.",
            MinimumLength = 1
        )]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(
            50,
            ErrorMessage = "Last name must be between 1 and 50 characters.",
            MinimumLength = 1
        )]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(254, ErrorMessage = "Email must be 254 characters or less.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(
            20,
            ErrorMessage = "Phone must be between 8 and 20 characters.",
            MinimumLength = 8
        )]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Position is required")]
        [StringLength(
            100,
            ErrorMessage = "Position must be between 1 and 100 characters.",
            MinimumLength = 1
        )]
        public string Position { get; set; } = string.Empty;
    }
}
