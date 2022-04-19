using System.ComponentModel.DataAnnotations;

namespace CarFactory.Models
{
    public sealed record BuildCarInputModelItem(
        [property: Required] int Amount,
        [property: Required] CarSpecificationInputModel Specification
    );
}