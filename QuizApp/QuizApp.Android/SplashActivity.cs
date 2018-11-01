using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
namespace QuizApp.Droid
{
    [Activity(Label = "Quiz App", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(async () =>
            {
                await Task.Delay(300);
            });

            startupWork.ContinueWith(t =>
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));

            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
    }
}