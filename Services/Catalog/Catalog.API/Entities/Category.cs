﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Catalog.API.Entities;

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}