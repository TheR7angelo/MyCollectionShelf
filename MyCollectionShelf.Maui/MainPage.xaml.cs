namespace MyCollectionShelf.Maui;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
        // CameraView.CamerasLoaded += CameraView_CamerasLoaded;
        // CameraView.BarcodeDetected += CameraView_BarcodeDetected;
    }

    // private void CameraView_CamerasLoaded(object sender, EventArgs e)
    // {
    //     if (CameraView.NumCamerasDetected > 0)
    //     {
    //         if (CameraView.NumMicrophonesDetected > 0) CameraView.Microphone = CameraView.Microphones.First();
    //         CameraView.Camera = CameraView.Cameras.First();
    //         MainThread.BeginInvokeOnMainThread(async () =>
    //         {
    //             await CameraView.StartCameraAsync();
    //             // if (await CameraView.StartCameraAsync() == CameraResult.Success)
    //             // {
    //             //     
    //             // }
    //         });
    //     }
    // }
}