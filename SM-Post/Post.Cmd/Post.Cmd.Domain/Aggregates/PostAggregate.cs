using CQRS.Core.Domain;
using Post.Common.Events;

namespace Post.Cmd.Domain.Aggregates
{
    public class PostAggregate: AggregateRoot
    {
        private bool _active;
        private string _author;
        private readonly Dictionary<Guid, Tuple<string, string>> _comments = new ();

        public bool Active {
            get => _active; set => _active = value;
        }

        public PostAggregate(){

        }

        public PostAggregate(Guid id, string author, string message) {
            RaiseEvent(new PostCreatedEvent {
                Id = id,
                Author = author,
                Message = message
            });
        }
    }
}