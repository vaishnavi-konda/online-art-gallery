using System;
using System.Collections.Generic;

namespace ARTGALLERYRESTSERVICE.Models.Db;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string? UserPhone { get; set; }

    public string UserAddress { get; set; } = null!;

    public string? UserRole { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
