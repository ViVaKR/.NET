using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Repos.Models;

public partial class TodoItem
{
    [Key]
    public long Id { get; set; }

    public string? Name { get; set; }

    public bool IsComplete { get; set; }
}
