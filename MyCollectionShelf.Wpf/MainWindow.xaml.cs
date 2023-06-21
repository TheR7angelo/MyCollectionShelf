using MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;
using MyCollectionShelf.Wpf.Shelf.Book.Ui.UserControls;

namespace MyCollectionShelf.Wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Frame.Content = new AddEditBook();
            // Frame.Content = new UserControl1();
        }
    }
}