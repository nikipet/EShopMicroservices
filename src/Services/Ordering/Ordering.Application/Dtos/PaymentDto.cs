namespace Ordering.Application.Dtos;

public record PaymentDto(
    string? CardName,
    string CardNumber,
    string ExpirationDate,
    string Cvv,
    int PaymentMethod);