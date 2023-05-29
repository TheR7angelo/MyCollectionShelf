using System.Collections.Generic;
using System.Linq;
using DirectShowLib;
using MyCollectionShelf.Camera.Object.Structures;

namespace MyCollectionShelf.Camera.Object.Static_Class;

public static class CameraHelper
{
    public static IEnumerable<SVideoCaptureEnum> GetAvailableCameras()
    {
        var devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
        return devices.Select((t, i) => new SVideoCaptureEnum { Name = t.Name, Index = i });
    }
}