using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using UIKit;

namespace ColocatorFormsSample.iOS
{
    public class Application
    {
        static void Main(string[] args)
        {
            AskForLocationPermission();
            UIApplication.Main(args, null, "AppDelegate");
        }

        static private async void AskForLocationPermission()
        {
            PermissionStatus status = await CrossPermissions.Current.RequestPermissionAsync<LocationAlwaysPermission>();
        }
    }
}
