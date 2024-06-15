using CQRS.Core.Events;
namespace Post.Common.Events
{
    public class CommentRemovedEvent : BaseEvent
    {
        protected CommentRemovedEvent(): base(nameof(CommentRemovedEvent)){}
        
        public Guid CommentId { get; set; }
    }
}