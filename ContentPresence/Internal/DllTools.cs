﻿using System;
using System.Runtime.InteropServices;

namespace WeatherElectric.ContentPresence.Internal;

internal static class DllTools
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadLibrary(string dllToLoad);

    [DllImport("kernel32.dll")]
    public static extern bool FreeLibrary(IntPtr hModule);
}