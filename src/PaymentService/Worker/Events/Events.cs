namespace Worker.Events;

public record CreatedOrderEvent(Guid OrderId, decimal Value);
public record ApprovedPaymentEvent(Guid OrderId);
public record PaymentRejectedEvent(Guid OrderId, string Note);
public record CanceledOrderEvent(Guid OrderId, string Note);