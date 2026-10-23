namespace Core.Models;

public record class Address
{
    public required string StreetAddress { get; set; }
    public required string PostalCode { get; set; }
    public required string Town { get; set; }
    public required string Country { get; set; }

    public override string ToString()
    {
        return $"{StreetAddress}, {PostalCode}, {Town}, {Country}";
    }
}
