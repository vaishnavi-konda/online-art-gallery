using System;
using System.Collections.Generic;

namespace ARTGALLERYRESTSERVICE.Models.Db;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Artist { get; set; }

    public int? Stock { get; set; }

    public int? CategoryId { get; set; }

    public decimal Price { get; set; }

    public string? ProductDescription { get; set; }

    public string? ImageName { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
