using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Views;

namespace TextNovelReader.Platforms.Android;

#pragma warning disable CA1416

public static class FullScreenHelper
{
    public static void EnterFullScreen(Activity activity)
    {
        if (activity.Window == null)
            return;

        activity.Window.SetFlags(
            WindowManagerFlags.Fullscreen, 
            WindowManagerFlags.Fullscreen);


        //activity.Window.DecorView.WindowInsetsController?.Hide(
        //    WindowInsets.Type.Ime() |
        //    WindowInsets.Type.NavigationBars()
        //    );
    }

    public static void ExitFullScreen(Activity activity)
    {
        if (activity.Window == null)
            return;

        activity.Window.ClearFlags(WindowManagerFlags.Fullscreen);
        //activity.Window.DecorView.WindowInsetsController?.Show(
        //    WindowInsets.Type.Ime() |
        //    WindowInsets.Type.NavigationBars());
    }
}

#pragma warning restore CA1416