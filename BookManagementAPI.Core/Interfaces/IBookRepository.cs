using System.Collections.Generic;
using System.Threading.Tasks;
using BookManagementAPI.Core.Models;

namespace BookManagementAPI.Core.Interfaces

{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<IEnumerable<Book>> GetBooksByCategory(string category);
        Task<IEnumerable<Book>> GetBooksByAuthor(string author);
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
    }
}