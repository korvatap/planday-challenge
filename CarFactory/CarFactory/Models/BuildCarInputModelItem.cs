using System.ComponentModel.DataAnnotations;

namespace CarFactory.Models
{
    public sealed class BuildCarInputModelItem
    {
        [Required] public int Amount { get; init; }
        [Required] public CarSpecificationInputModel Specification { get; init; }
    }
}