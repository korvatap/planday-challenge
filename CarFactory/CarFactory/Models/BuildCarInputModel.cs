using System.Collections.Generic;

namespace CarFactory.Models
{
    public sealed record BuildCarInputModel(IEnumerable<BuildCarInputModelItem> Cars);
}