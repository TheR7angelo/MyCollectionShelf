namespace MyCollectionShelf.Book;

public interface IBookApi
{
    public Task<Object.Class.Book?> GetBookInformation(string isbn13);
}