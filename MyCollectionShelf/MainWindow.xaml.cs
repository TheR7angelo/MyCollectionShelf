using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Book.Object.Class;

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

        private MyCollectionShelf.Book.Object.Class.Book _bookTest = new();
        
        public MyCollectionShelf.Book.Object.Class.Book BookTest
        {
            get => _bookTest;
            set
            {
                _bookTest = value;
                OnPropertyChanged();
            }
        }
        

        public MainWindow()
        {
            // DecodingOptions = new DecodingOptions
            // {
            //     TryHarder = true,
            //     PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.EAN_13 }
            // };

            InitializeComponent();

            BookTest.BookInformations = new BookInformations
            {
                Title = "Livre de test yolo",
                Authors = new List<BookAuthors>
                {
                    new() {  }
                }
            };

            // VideoPreview.ResultFound += VideoPreviewResultFound;
            //
            // var devices = CameraHelper.GetAvailableCameras();
            // VideoPreview.StartCamera(devices.First());
        }

        // private async void VideoPreviewResultFound(Camera.Ui.Camera sender, Result result)
        // {
        //     Console.WriteLine(result.Text);
        //
        //     if (result.BarcodeFormat != BarcodeFormat.EAN_13) return;
        //
        //     var api = new OpenLibraryApi();
        //
        //     var book = await api.GetBookInformation(result.Text);
        //
        //     var success = await book?.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge,
        //         $"{book.BookInformations.Title}.jpg")!;
        //
        //     Console.WriteLine($"cover download {success}");
        // }
    }
}