using System;
using System.Linq;
using Foundation;
using UIKit;

namespace ColocatorFormsSample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            Colocator.ColocatorMain.Instance.StartWithAppKey("YOUR_APP_KEY");

            var authOptions = UserNotifications.UNAuthorizationOptions.Alert | UserNotifications.UNAuthorizationOptions.Badge | UserNotifications.UNAuthorizationOptions.Sound;
            UserNotifications.UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => { });
            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            return base.FinishedLaunching(app, options);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            NSString source = userInfo.ObjectForKey(new NSString("source")) as NSString;

            if (source == "colocator")
            {
                Colocator.ColocatorMain.Instance.ReceivedSilentNotificationWithUserInfo(userInfo, "CC_APP_KEY", completion: (result) => {
                    if (result)
                    {
                        completionHandler(UIBackgroundFetchResult.NewData);
                    }
                    else
                    {
                        completionHandler(UIBackgroundFetchResult.NoData);
                    }
                });
            }
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            byte[] bytes = deviceToken.ToArray<byte>();
            string[] hexArray = bytes.Select(b => b.ToString("x2")).ToArray();
            var token = string.Join(string.Empty, hexArray);

            Colocator.ColocatorMain.Instance.AddAliasWithKey("apns_user_id", token);
        }

        [Export("application:performFetchWithCompletionHandler:")]
        public override void PerformFetch(UIApplication application, System.Action<UIBackgroundFetchResult> completionHandler)
        {
            Colocator.ColocatorMain.Instance.UpdateLibraryBasedOnClientStatusWithClientKey("CC_APP_KEY", false, completion: (result) =>
            {
                if (result)
                {
                    completionHandler(UIBackgroundFetchResult.NewData);
                }
                else
                {
                    completionHandler(UIBackgroundFetchResult.NoData);
                }
            });
        }
    }
}
