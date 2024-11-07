namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string ExpirationDate { get; } = default!;
    public string Cvv { get; } = default!;
    public int PaymentMethod { get; } = default!;

    protected Payment()
    {
    }

    private Payment(string? cardName, string cardNumber, string expirationDate, string cvv, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string? cardName, string cardNumber, string expirationDate, string cvv,
        int paymentMethod)
    {
        ArgumentNullException.ThrowIfNull(cardNumber);
        ArgumentNullException.ThrowIfNull(expirationDate);
        ArgumentNullException.ThrowIfNull(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

        return new Payment(cardName, cardNumber, expirationDate, cvv, paymentMethod);
    }
}