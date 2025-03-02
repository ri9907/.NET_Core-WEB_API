using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities;

public partial class Order
{
    public int OrderId { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public int OrderSum { get; set; }

    [Required]
    public int UserId { get; set; }

    public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    public virtual User? User { get; set; } = null!;
}
