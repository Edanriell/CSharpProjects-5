﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Keyless]
public partial class CurrentProductList
{
    public int ProductId { get; set; }

    [StringLength(40)]
    public string ProductName { get; set; } = null!;
}