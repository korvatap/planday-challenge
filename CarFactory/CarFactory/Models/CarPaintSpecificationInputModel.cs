namespace CarFactory.Models
{
    public sealed record CarPaintSpecificationInputModel(
        string Type,
        string BaseColor,
        string? StripeColor,
        string? DotColor
    );
}