using MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;

namespace MyCollectionShelf.Wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            // Frame.Content = new AddEditBook();
            Frame.Content = new BookShelf();
        }
    }
}