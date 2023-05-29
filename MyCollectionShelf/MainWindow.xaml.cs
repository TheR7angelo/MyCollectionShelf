using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace MyCollectionShelf
{

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var lst = new List<VideoCapture>();

            var i = 0;
            while (true)
            {
                using var capture = new VideoCapture(i);
                if (!capture.IsOpened()) break;
                lst.Add(capture);
                i++;
                
            }
            
            Console.WriteLine(lst.Count);
        }
    }
}