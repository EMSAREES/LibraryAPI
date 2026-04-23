using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Events.Books;


namespace LibraryAPI.Domain.Entities;

/// <summary>
/// Tabla pivot que relaciona un libro con sus autores.
/// Permite que un libro tenga múltiples autores con distintos roles
/// (autor principal, coautor, traductor, editor).
/// </summary>
public sealed class BookAuthor : BaseEntity
{
    /// <summary>
    /// Identificador del libro.
    /// </summary>
    public Guid BookId { get; private set; }

    /// <summary>
    /// Identificador del autor.
    /// </summary>
    public Guid AuthorId { get; private set; }

    /// <summary>
    /// Rol del autor en este libro específico.
    /// Un mismo autor puede ser traductor en un libro y autor principal en otro.
    /// </summary>
    public BookAuthorRole Role { get; private set; }

    /// <summary>
    /// Navegación al libro relacionado.
    /// </summary>
    public Book Book { get; private set; } = null!;

    /// <summary>
    /// Navegación al autor relacionado.
    /// </summary>
    public Author Author { get; private set; } = null!;

    // Constructor privado para EF Core
    private BookAuthor() { }

    private BookAuthor(Guid bookId, Guid authorId, BookAuthorRole role)
    {
        BookId = bookId;
        AuthorId = authorId;
        Role = role;
    }

    /// <summary>
    /// Crea una nueva relación entre un libro y un autor con su rol.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si alguno de los identificadores es inválido.
    /// </exception>
    public static BookAuthor Create(Guid bookId, Guid authorId, BookAuthorRole role)
    {
        if (bookId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(BookId)
            };

        if (authorId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(AuthorId)
            };

        var bookAuthor = new BookAuthor(bookId, authorId, role);
        bookAuthor.AddDomainEvent(new BookAuthorCreatedEvent(bookAuthor));
        return bookAuthor;
    }
}