using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteDB;

public class Country
{
    [Key]
    [Column("country_id")]
    public int CId { get; set; }
    public string Name { get; set; } = "";
}

