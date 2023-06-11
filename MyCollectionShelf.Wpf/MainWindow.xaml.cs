using MyCollectionShelf.Wpf.Ui.Book.Pages;

namespace MyCollectionShelf.Wpf
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