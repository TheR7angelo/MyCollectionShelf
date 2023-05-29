﻿using System.Linq;
using MyCollectionShelf.Camera.Object.Static_Class;

namespace MyCollectionShelf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var devices = CameraHelper.GetAvailableCameras();
            VideoPreview.StartCamera(devices.First());
        }
    }
}