using MyCollectionShelf.Ui.Book.Pages;

namespace MyCollectionShelf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Frame.Content = new AddEditBook();
        }
    }
}