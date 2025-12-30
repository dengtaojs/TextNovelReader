using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using TextNovelReader.Service;

namespace TextNovelReader;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public static MainActivity? Current { get; set; }
    public void RegisterEvents()
    {
#pragma warning disable CA1416
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
        {
            OnBackInvokedDispatcher.RegisterOnBackInvokedCallback(0, new BackCallback());
        }
#pragma warning restore CA1416
    }

}

public class BackCallback : Java.Lang.Object, Android.Window.IOnBackInvokedCallback
{
    public void OnBackInvoked()
    {
        if(Shell.Current?.CurrentPage is IBackButtonHandler handler)
        {
            handler.OnSystemBackButtonPressed(); 
        }
        else
        {
            GoToHome(); 
        }
    }

    private static void GoToHome()
    {
        var intent = new Intent(Intent.ActionMain);
        intent.AddCategory(Intent.CategoryHome);
        intent.SetFlags(ActivityFlags.NewTask);

        Android.App.Application.Context.StartActivity(intent); 
    }
}


