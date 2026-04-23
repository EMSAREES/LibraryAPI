using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Categories;

public sealed class CategoryUpdatedEvent : BaseDomainEvent
{
    public Category Category { get; }

    public CategoryUpdatedEvent(Category category)
    {
        Category = category;
    }
}
