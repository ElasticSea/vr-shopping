using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR;

public static class XrExtensions
{
    public static List<InputDevice> GetDevices(this InputDeviceCharacteristics characteristics)
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
        return devices;
    }

    public static InputDevice GetDevice(this InputDeviceCharacteristics characteristics)
    {
        return characteristics.GetDevices().FirstOrDefault();
    }

    public static bool GetFeatureValue(this InputDevice device, InputFeatureUsage<bool> feature)
    {
        return device.TryGetFeatureValue(feature, out bool value) && value;
    }
}