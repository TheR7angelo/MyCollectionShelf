using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Ui.Book.Pages;

namespace MyCollectionShelf
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // private DecodingOptions _decodingOption = new();
        //
        // public DecodingOptions DecodingOptions
        // {
        //     get => _decodingOption;
        //     set
        //     {
        //         _decodingOption = value;
        //         OnPropertyChanged();
        //     }
        // }
        //
        // private MyCollectionShelf.Book.Object.Class.Book _bookTest = new();
        //
        // public MyCollectionShelf.Book.Object.Class.Book BookTest
        // {
        //     get => _bookTest;
        //     set
        //     {
        //         _bookTest = value;
        //         OnPropertyChanged();
        //     }
        // }
        

        public MainWindow()
        {
            // DecodingOptions = new DecodingOptions
            // {
            //     TryHarder = true,
            //     PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.EAN_13 }
            // };

            InitializeComponent();

            Frame.Content = new AddEditBook();

            // BookTest.BookInformations = new BookInformations
            // {
            // Title = "Livre de test yolo",
            // Authors = new ObservableCollection<BookAuthors>
            // {
            //     new() { FamilyName = "First" },
            //     new() { FamilyName = "Second" }
            // },
            // Summarize = ""
            // };

            // VideoPreview.ResultFound += VideoPreviewResultFound;
            //
            // var devices = CameraHelper.GetAvailableCameras();
            // VideoPreview.StartCamera(devices.First());
        }

        // private async void VideoPreviewResultFound(Camera.Ui.Camera sender, Result result)
        // {
            // if (result.BarcodeFormat != BarcodeFormat.EAN_13) return;
            //
            // Console.WriteLine(result.Text);
            // var api = new BookAllApi();
            // var book = await api.GetBookInformation(result.Text);
            //
            // Console.WriteLine(book.BookInformations.Title);
            
            // var api = new OpenLibraryApi();
            //
            // var book = await api.GetBookInformation(result.Text);
            //
            // var success = await book?.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge,
            //     $"{book.BookInformations.Title}.jpg")!;
            //
            // Console.WriteLine($"cover download {success}");
        // }
    }
}