namespace MyCollectionShelf.Book;

public interface IBookApi
{
    public Task<Sql.Object.Book.Class.Table.Book?> GetBookInformation(string isbn13);
}