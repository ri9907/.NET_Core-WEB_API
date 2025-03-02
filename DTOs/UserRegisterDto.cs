using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs
{
    public class UserRegisterDto
    {
            [Required, EmailAddress]
            public string Email { get; set; } = null!;

            [Required]
            public string FirstName { get; set; } = null!;

            [Required]
            public string LastName { get; set; } = null!;

            [Required]
            public string Password { get; set; } = null!;
    }
}
