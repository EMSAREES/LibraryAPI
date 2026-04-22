using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Exceptions.Books;
using LibraryAPI.Domain.Events.Books;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; private set; } = default!;
    public Isbn ISBN { get; private set; } = default!;
    public DateTime PublicationDate { get; private set; }
    public BookLanguage? Language { get; private set; }
    public  int TotalPages { get; private set; }
    public Imagen? CoverImage { get; private set; }
    public Imagen? BackCoverImage { get; private set; }
    public bool IsActive { get; private set; }

    // Constructor privado para EF Core
    private Book() { }

    /// <summary>
    /// Constructor privado que inicializa un libro con sus propiedades esenciales.
    /// Se usa únicamente desde el método de fábrica <see cref="Create"/>
    /// para garantizar que la instancia se cree con todas las validaciones de negocio.
    /// </summary>
    private Book(
        string title,
        Isbn isbn,
        DateTime publicationDate,
        BookLanguage? language,
        int totalPages,
        Imagen? coverImage,
        Imagen? backCoverImage,
        Guid createdByUserId)
    {
        Title = title;
        ISBN = isbn;
        PublicationDate = publicationDate;
        Language = language;
        TotalPages = totalPages;
        CoverImage = coverImage;
        BackCoverImage = backCoverImage;
        IsActive = true;
        CreatedByUserId = createdByUserId;
    }

    /// <summary>
    /// Crea un nuevo libro validando que tenga título, ISBN y fecha de publicación.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si el título es nulo o vacío, el ISBN es inválido
    /// o la fecha de publicación es futura.
    /// </exception>
    public static Book Create(
        string title,
        Isbn isbn,
        DateTime publicationDate,
        BookLanguage? language,
        int totalPages,
        Imagen? coverImage,
        Imagen? backCoverImage,
        Guid createdByUserId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainValidationException(DomainErrors.Book.TitleRequired)
            {
                FieldName = nameof(Title)
            };

        if (isbn == default)
            throw new DomainValidationException(DomainErrors.Book.IsbnRequired)
            {
                FieldName = nameof(ISBN)
            };

        if (publicationDate.Date > DateTime.UtcNow.Date)
            throw new DomainValidationException(DomainErrors.General.InvalidValue)
            {
                FieldName = nameof(PublicationDate)
            };

        if (totalPages <= 0)
            throw new DomainValidationException(DomainErrors.General.InvalidValue)
            {
                FieldName = nameof(TotalPages)
            };

        var book = new Book(title, isbn, publicationDate, language, totalPages, coverImage, backCoverImage, createdByUserId);
        book.AddDomainEvent(new BookCreatedEvent(book));
        return book;
    }


    /// <summary>
    /// Actualiza los datos básicos del libro.
    /// </summary>
    public void Update(
        string title,
        Isbn isbn,
        DateTime publicationDate,
        BookLanguage? language,
        int totalPages,
        Imagen? coverImage,
        Imagen? backCoverImage)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainValidationException(DomainErrors.Book.TitleRequired)
            {
                FieldName = nameof(Title)
            };

        if (isbn == default)
            throw new DomainValidationException(DomainErrors.Book.IsbnRequired)
            {
                FieldName = nameof(ISBN)
            };

        if (publicationDate.Date > DateTime.UtcNow.Date)
            throw new DomainValidationException(DomainErrors.General.InvalidValue)
            {
                FieldName = nameof(PublicationDate)
            };

        if (totalPages <= 0)
            throw new DomainValidationException(DomainErrors.General.InvalidValue)
            {
                FieldName = nameof(TotalPages)
            };

        Title = title;
        ISBN = isbn;
        PublicationDate = publicationDate;
        Language = language;
        TotalPages = totalPages;
        CoverImage = coverImage;
        BackCoverImage = backCoverImage;

        MarkAsUpdated();
        AddDomainEvent(new BookUpdatedEvent(this));
    }

    /// <summary>
    /// Desactiva un libro. Un libro inactivo no puede prestarse ni reservarse.
    /// </summary>
    public void Deactivate()
    {
        if (!IsActive)
            throw new BookInactiveException();

        IsActive = false;

        MarkAsUpdated();
        AddDomainEvent(new BookDeactivatedEvent(this));
    }

    /// <summary>
    /// Activa un libro previamente desactivado.
    /// </summary>
    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;

        MarkAsUpdated();
    }
}