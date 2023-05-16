namespace EventBus.Messages.Events
{
    public class BaseIntegrationEvent
    {
        public Guid CorrelationId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public BaseIntegrationEvent(Guid correlationId, DateTime createdAt)
        {
            CorrelationId = correlationId;
            CreatedAt = createdAt;
        }

        public BaseIntegrationEvent()
        {
            CorrelationId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

    }
}
