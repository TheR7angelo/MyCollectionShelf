using MyCollectionShelf.Sql;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

namespace TestProjectMyCollectionShelf.Sql;

public class t
{
    public class Parent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Child> Children { get; set; }
    }

    public class Child
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(typeof(Parent))]
        public int ParentId { get; set; }
    }
    
    
    [Fact]
    private void Test()
    {
        using var handler = new SqlMainHandler();
        var db = handler.GetSqlConnection();
        
        db.CreateTable<Child>();
        db.CreateTable<Parent>();

        var parent = new Parent
        {
            Name = "test 2",
            Children = new List<Child>
            {
                new() { Name = "Child 1" },
                new() { Name = "Child 2" }
            }
        };

        db.InsertWithChildren(parent);

        var parentSql = db.GetWithChildren<Parent>(parent.Id);
        var children = parentSql.Children;
    }
}