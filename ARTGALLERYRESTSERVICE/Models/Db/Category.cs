using System;
using System.Collections.Generic;

namespace ARTGALLERYRESTSERVICE.Models.Db;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CatogoryName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
