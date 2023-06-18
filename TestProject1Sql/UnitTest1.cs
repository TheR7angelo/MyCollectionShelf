using MyCollectionShelf.Book.Object.Class;
using SQLite;

namespace TestProject1Sql;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var db = new SQLiteConnection("db.sqlite", SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadWrite);
        db.CreateTable<BookAuthors>();
    }
}


// public class Stock
//     {
//         [PrimaryKey, AutoIncrement]
//         public int Id { get; set; }
//         public string? Symbol { get; set; }
//     }
//
//     public class Valuation
//     {
//         [PrimaryKey, AutoIncrement]
//         public int Id { get; set; }
//         [Indexed]
//         public int StockId { get; set; }
//         public DateTime Time { get; set; }
//         public decimal Price { get; set; }
//     }
//     
// public class Bus
// {
//     [PrimaryKey, AutoIncrement]
//     public int Id { get; set; }
//
//     public string PlateNumber { get; set; }
//
//     [OneToMany]
//     public List<Person> Passengers { get; set; }
// }
//
// public class Person
// {
//     [PrimaryKey, AutoIncrement]
//     public int Id { get; set; }
//
//     public string Name { get; set; }
//
//     [ForeignKey(typeof(Bus))]
//     public int BusId { get; set; }
// }