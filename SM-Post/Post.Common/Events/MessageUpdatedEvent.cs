using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class MessageUpdatedEvent : BaseEvent
    {
        protected MessageUpdatedEvent(): base(nameof(MessageUpdatedEvent)){}

        public string Message {get; set;}
    }
}