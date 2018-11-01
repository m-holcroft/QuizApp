
using Android.Content;
using Android.Runtime;
using Android.Views;
using QuizApp.Droid.Classes;


[assembly: Xamarin.Forms.Dependency(typeof(CDeviceOrientationDroid))]
namespace QuizApp.Droid.Classes
{

    public class CDeviceOrientationDroid : Interfaces.IDeviceOrientation
    {
        public CDeviceOrientationDroid()
        {

        }

        public static void Init()
        {

        }

        public Interfaces.DeviceOrientations GetOrientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            SurfaceOrientation rotation = windowManager.DefaultDisplay.Rotation;
            bool isLandscape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;
            return isLandscape ? Interfaces.DeviceOrientations.Landscape : Interfaces.DeviceOrientations.Portrait;
        }
    }
}