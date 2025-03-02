using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities;

public partial class User
{
    public int UserId { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
}
