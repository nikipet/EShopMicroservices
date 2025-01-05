namespace Basket.API.Dtos;

public class BasketCheckoutDto
{
    // Customer information
    public string Username { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;

    // Bulling and Shipping address
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;

    // Payment information
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string ExpirationDate { get; } = default!;
    public string Cvv { get; } = default!;
    public int PaymentMethod { get; } = default!;
}