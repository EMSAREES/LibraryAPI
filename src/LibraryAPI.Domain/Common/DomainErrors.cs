namespace LibraryAPI.Domain.Common;

/// <summary>
/// Centraliza todos los mensajes de error del dominio.
/// Evita strings duplicados en validaciones y excepciones.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    /// Errores generales reutilizables en cualquier entidad.
    /// </summary>
    public static class General
    {
        public const string RequiredFieldNull   = "The required field cannot be null.";
        public const string InvalidGuid         = "The provided identifier is not valid.";
        public const string InvalidDateRange    = "The start date cannot be greater than the end date.";
        public const string InvalidValue   = "El valor proporcionado no es válido.";
    }

    /// <summary>
    /// Errores relacionados con validaciones de campos.
    /// </summary>
    public static class Validation
    {
        public const string ValueRequired       = "The value is required and cannot be empty.";
        public const string StringTooLong       = "The text exceeds the maximum allowed length.";
        public const string NegativeValue       = "The value cannot be negative.";
    }

    /// <summary>
    /// Errores del ciclo de vida de un préstamo.
    /// </summary>
    public static class Loan
    {
        public const string UserIsBlocked       = "The user is blocked due to pending fines.";
        public const string MaxLoansReached     = "The user has reached the maximum number of simultaneous loans.";
        public const string BookNotAvailable    = "The book is not available at this branch.";
        public const string AlreadyReturned     = "This loan has already been closed as returned.";
        public const string CannotCancel        = "The loan cannot be canceled in its current state.";
        public const string LoanNotFound        = "The requested loan was not found.";
        public const string CannotReturn        = "The loan cannot be marked as returned in its current state.";
    }

    /// <summary>
    /// Errores relacionados con multas y sanciones.
    /// </summary>
    public static class Fine
    {
        public const string AlreadyPaid         = "This fine has already been paid.";
        public const string InvalidAmount       = "The fine amount must be greater than zero.";
        public const string FineNotFound        = "The requested fine was not found.";
        public const string UserHasUnpaidFines  = "The user has outstanding fines and cannot perform new transactions.";
    }

    /// <summary>
    /// Errores relacionados con reservas de libros.
    /// </summary>
    public static class Reservation
    {
        public const string AlreadyPending      = "The user already has an active reservation for this book.";
        public const string CannotFulfill       = "The reservation cannot be fulfilled in its current state.";
        public const string CannotCancel        = "The reservation cannot be canceled in its current state.";
        public const string AlreadyExpired      = "The reservation has already expired and cannot be modified.";
        public const string ReservationNotFound = "The requested reservation was not found.";
    }
    /// <summary>
    /// Errores relacionados con el inventario y stock de libros.
    /// </summary>
    public static class Stock
    {
        public const string InsufficientCopies = "There are not enough copies available to perform this operation.";
        public const string NegativeStock      = "The available stock cannot be negative.";
        public const string ExceedsTotalCopies = "The available copies cannot exceed the total registered.";
    }

    /// <summary>
    /// Errores relacionados con los datos de un libro.
    /// </summary>
    public static class Book
    {
        public const string TitleRequired      = "The book title is required.";
        public const string IsbnRequired       = "The book ISBN is required.";
        public const string MustHaveAuthor     = "The book must have at least one registered author.";
        public const string IsInactive         = "The book is inactive and cannot be operated.";
        public const string BookNotFound       = "The requested book was not found.";
    }

    /// <summary>
    /// Errores relacionados con usuarios del sistema.
    /// </summary>
    public static class User
    {
        public const string EmailAlreadyInUse   = "The email address is already registered in the system.";
        public const string InvalidCredentials  = "The provided credentials are incorrect.";
        public const string NotFound            = "The user was not found.";
        public const string AlreadyBlocked      = "The user is already blocked.";
        public const string NotBlocked          = "The user is not currently blocked.";
        public const string LibraryCardRequired = "A library card is required for client users.";
        public const string NotActive           = "The user account is not active.";
        public const string UserIsBlocked       = "The user is blocked and cannot perform this operation.";
        public const string AlreadyActive       = "The user is already active in the system.";
        
    }

    /// <summary>
    /// Errores relacionados con sucursales.
    /// </summary>
    public static class Branch
    {
        public const string NotFound            = "The branch was not found.";
        public const string IsInactive          = "The branch is inactive and cannot process loans.";
    }
    
}