using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Authors;

public sealed class AuthorCreatedEvent : BaseDomainEvent
{
    public Author Author { get; }

    public AuthorCreatedEvent(Author author)
    {
        Author = author;
    }
}
