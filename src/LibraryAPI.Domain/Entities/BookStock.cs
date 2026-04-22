using LibraryAPI.Domain.Events.Stock;
using LibraryAPI.Domain.Exceptions.Stock;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;

namespace LibraryAPI.Domain.Entities;

/// <summary>
/// Representa el inventario de un libro específico en una sucursal.
/// Controla las copias totales y disponibles para préstamo.
/// Su clave primaria es un Id propio, pero la combinación
/// BookId + BranchId debe ser única en base de datos.
/// </summary>
public sealed class BookStock : BaseEntity
{
    /// <summary>
    /// Identificador del libro al que pertenece este stock.
    /// </summary>
    public Guid BookId { get; private set; }

    /// <summary>
    /// Identificador de la sucursal donde se encuentra este stock.
    /// </summary>
    public Guid BranchId { get; private set; }

    /// <summary>
    /// Total de copias físicas registradas en esta sucursal.
    /// Solo cambia cuando el admin agrega o da de baja ejemplares físicos.
    /// </summary>
    public int TotalCopies { get; private set; }

    /// <summary>
    /// Copias actualmente disponibles para préstamo.
    /// Se decrementa al crear un préstamo y se incrementa al devolver.
    /// Siempre debe estar entre 0 y TotalCopies.
    /// </summary>
    public int AvailableCopies { get; private set; }

    /// <summary>
    /// Navegación al libro relacionado.
    /// </summary>
    public Book Book { get; private set; } = null!;

    /// <summary>
    /// Navegación a la sucursal relacionada.
    /// </summary>
    public Branch Branch { get; private set; } = null!;

    // Constructor privado para EF Core
    private BookStock() { }

    private BookStock(Guid bookId, Guid branchId, int totalCopies)
    {
        BookId = bookId;
        BranchId = branchId;
        TotalCopies = totalCopies;
        AvailableCopies = totalCopies;
    }

    /// <summary>
    /// Crea un nuevo registro de stock para un libro en una sucursal.
    /// Las copias disponibles arrancan iguales al total.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si los identificadores son inválidos o el total es negativo.
    /// </exception>
    public static BookStock Create(Guid bookId, Guid branchId, int totalCopies)
    {
        if (bookId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(BookId)
            };

        if (branchId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(BranchId)
            };

        if (totalCopies < 0)
            throw new DomainValidationException(DomainErrors.Validation.NegativeValue)
            {
                FieldName = nameof(TotalCopies)
            };

        return new BookStock(bookId, branchId, totalCopies);
    }

    /// <summary>
    /// Decrementa las copias disponibles cuando se crea un préstamo.
    /// </summary>
    /// <exception cref="InsufficientStockException">
    /// Se lanza si no hay copias disponibles para prestar.
    /// </exception>
    public void Decrement()
    {
        if (AvailableCopies <= 0)
            throw new InsufficientStockException(AvailableCopies);

        AvailableCopies--;

        MarkAsUpdated();
        AddDomainEvent(new StockDecreasedEvent(this, 1));
    }

    /// <summary>
    /// Incrementa las copias disponibles cuando un libro es devuelto.
    /// </summary>
    /// <exception cref="StockExceedsTotalCopiesException">
    /// Se lanza si el incremento superaría el total de copias físicas.
    /// </exception>
    public void Increment()
    {
        if (AvailableCopies >= TotalCopies)
            throw new StockExceedsTotalCopiesException();

        AvailableCopies++;

        MarkAsUpdated();
        AddDomainEvent(new StockIncreasedEvent(this, 1));
    }

    /// <summary>
    /// Agrega copias físicas nuevas al inventario de la sucursal.
    /// Úsalo cuando el admin registra ejemplares recién adquiridos.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si la cantidad a agregar es menor o igual a cero.
    /// </exception>
    public void AddCopies(int copies)
    {
        if (copies <= 0)
            throw new DomainValidationException(DomainErrors.Validation.NegativeValue)
            {
                FieldName = nameof(copies)
            };

        TotalCopies += copies;
        AvailableCopies += copies;

        MarkAsUpdated();
        AddDomainEvent(new StockIncreasedEvent(this, copies));
    }

    /// <summary>
    /// Da de baja copias físicas del inventario de la sucursal.
    /// Úsalo cuando el admin retira ejemplares dañados o perdidos.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si la cantidad a retirar es menor o igual a cero.
    /// </exception>
    /// <exception cref="NegativeStockException">
    /// Se lanza si el retiro resultaría en stock negativo.
    /// </exception>
    public void RemoveCopies(int copies)
    {
        if (copies <= 0)
            throw new DomainValidationException(DomainErrors.Validation.NegativeValue)
            {
                FieldName = nameof(copies)
            };

        if (copies > AvailableCopies)
            throw new NegativeStockException();

        TotalCopies -= copies;
        AvailableCopies -= copies;

        MarkAsUpdated();
        AddDomainEvent(new StockDecreasedEvent(this, copies));
    }

    /// <summary>
    /// Indica si hay al menos una copia disponible para préstamo.
    /// </summary>
    public bool HasAvailableCopies => AvailableCopies > 0;
}