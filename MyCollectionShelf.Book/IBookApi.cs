namespace MyCollectionShelf.Book;

public interface IBookApi
{
    public Task<Sql.Table.Book.Book?> GetBookInformation(string isbn13);
}