using System.ComponentModel.DataAnnotations.Schema;

namespace ScopedWorker.Entities;

[Table("Clientes")]
public class Cliente
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}
