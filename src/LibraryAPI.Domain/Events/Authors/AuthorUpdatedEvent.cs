using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Authors;

public sealed class AuthorUpdatedEvent : BaseDomainEvent
{
    public Author Author { get; }

    public AuthorUpdatedEvent(Author author)
    {
        Author = author;
    }
}
