namespace Entities.Exceptions
{
    
    //sealed, classın kalıtılmasını önler
    public sealed class BookNotFoundException : NotFoundExceptionException
    {
        public BookNotFoundException(int id) : base($"The book with id: {id} cloud not found.")
        {
        }
    }

}
