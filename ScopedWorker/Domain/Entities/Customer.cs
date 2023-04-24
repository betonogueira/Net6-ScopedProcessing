using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ScopedWorker.Domain.Entities;

[Table("Clientes")]
public class Customer
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}
