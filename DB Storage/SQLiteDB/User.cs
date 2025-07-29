using System;
using System.Collections.Generic;

namespace SQLiteDB;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int Age { get; set; }

    public string? Position { get; set; }

    public bool IsMarried { get; set; }
    public Country? Country { get; set; }
}
