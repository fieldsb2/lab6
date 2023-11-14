using Blazor_Lab_Starter_Code;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor_Lab_Starter_Code
{
    public class LibraryService
    {
        private List<Book> books = new List<Book>();
        private List<User> users = new List<User>();
        private Dictionary<User, List<Book>> borrowedBooks = new Dictionary<User, List<Book>>();


        public List<Book> GetBooks() => books;

        public List<User> GetUsers() => users;

        public Dictionary<User, List<Book>> GetBorrowedBooks() => borrowedBooks;

        public void AddBook(Book book)
        {
            if (book != null)
            {
                book.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
                books.Add(book);
            }
        }

        public void EditBook(Book updatedBook)
        {
            if (updatedBook != null)
            {
                Book existingBook = books.FirstOrDefault(b => b.Id == updatedBook.Id);

                if (existingBook != null)
                {
                    existingBook.Title = updatedBook.Title;
                    existingBook.Author = updatedBook.Author;
                    existingBook.ISBN = updatedBook.ISBN;
                }
            }
        }

        public void DeleteBook(int bookId)
        {
            Book bookToDelete = books.FirstOrDefault(b => b.Id == bookId);

            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
            }
        }

        public void AddUser(User user)
        {
            if (user != null)
            {
                user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
                users.Add(user);
            }
        }

        public void EditUser(User updatedUser)
        {
            if (updatedUser != null)
            {
                User existingUser = users.FirstOrDefault(u => u.Id == updatedUser.Id);

                if (existingUser != null)
                {
                    existingUser.Name = updatedUser.Name;
                    existingUser.Email = updatedUser.Email;
                }
            }
        }

        public void DeleteUser(int userId)
        {
            User userToDelete = users.FirstOrDefault(u => u.Id == userId);

            if (userToDelete != null)
            {
                users.Remove(userToDelete);
            }
        }

        public void BorrowBook(int bookId, int userId)
        {
            Book bookToBorrow = books.FirstOrDefault(b => b.Id == bookId);
            User userBorrowing = users.FirstOrDefault(u => u.Id == userId);

            if (bookToBorrow != null && userBorrowing != null)
            {
                if (!borrowedBooks.ContainsKey(userBorrowing))
                {
                    borrowedBooks[userBorrowing] = new List<Book>();
                }

                borrowedBooks[userBorrowing].Add(bookToBorrow);
                books.Remove(bookToBorrow);
            }
        }

        public void ReturnBook(int userId, int bookNumber)
        {
            User userReturning = users.FirstOrDefault(u => u.Id == userId);

            if (userReturning != null && borrowedBooks.ContainsKey(userReturning) && borrowedBooks[userReturning].Count > 0)
            {
                if (bookNumber >= 1 && bookNumber <= borrowedBooks[userReturning].Count)
                {
                    Book bookToReturn = borrowedBooks[userReturning][bookNumber - 1];
                    borrowedBooks[userReturning].RemoveAt(bookNumber - 1);
                    books.Add(bookToReturn);
                }
            }
        }
    }
}