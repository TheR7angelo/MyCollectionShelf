using MyCollectionShelf.Wpf.Ui.Book.UserControls;

namespace MyCollectionShelf.Wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Frame.Content = new UserControl1();
        }
    }
}