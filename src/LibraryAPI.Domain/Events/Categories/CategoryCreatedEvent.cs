using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Categories;

public sealed class CategoryCreatedEvent : BaseDomainEvent
{
    public Category Category { get; }

    public CategoryCreatedEvent(Category category)
    {
        Category = category;
    }
}
