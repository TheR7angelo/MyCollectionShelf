using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Camera.Object.Static_Class;
using MyCollectionShelf.WebApi.Book;
using MyCollectionShelf.WebApi.Object.Enum;
using ZXing;
using ZXing.Common;

namespace MyCollectionShelf
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        private DecodingOptions _decodingOption = new();

        public DecodingOptions DecodingOptions
        {
            get => _decodingOption;
            set
            {
                _decodingOption = value;
                OnPropertyChanged();
            }
        }


        public MainWindow()
        {
            DecodingOptions = new DecodingOptions
            {
                TryHarder = true,
                PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.EAN_13 }
            };

            InitializeComponent();

            VideoPreview.ResultFound += VideoPreviewResultFound;
            
            var devices = CameraHelper.GetAvailableCameras();
            VideoPreview.StartCamera(devices.First());
        }

        private async void VideoPreviewResultFound(Camera.Ui.Camera sender, Result result)
        {
            Console.WriteLine(result.Text);

            var api = new OpenLibraryApi();

            if (result.BarcodeFormat != BarcodeFormat.EAN_13) return;

            var book = await api.GetBookInformation(result.Text);
            
            await api.GetCovers(result.Text, $"{book?.Title}.jpg", EBookSize.Medium);

            Console.WriteLine("cover download");
        }
    }
}