using Xamarin.Forms;

[assembly: Dependency(typeof(QuizApp.UWP.Classes.CDeviceOrentationUWP))]
namespace QuizApp.UWP.Classes
{
    public class CDeviceOrentationUWP : Interfaces.IDeviceOrientation
    {
        public CDeviceOrentationUWP() { }

        public Interfaces.DeviceOrientations GetOrientation()
        {
            var orientation = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Orientation;
            if (orientation == Windows.UI.ViewManagement.ApplicationViewOrientation.Landscape)
            {
                return Interfaces.DeviceOrientations.Landscape;
            }
            else
            {
                return Interfaces.DeviceOrientations.Portrait;
            }
        }
    }
}
