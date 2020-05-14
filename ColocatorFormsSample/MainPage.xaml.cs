using System;
using System.ComponentModel;
using Colocator;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ColocatorFormsSample
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, ColocatorDelegate
    {
        IColocator colo = ColocatorMain.Instance;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Click_Start_Colocator(object sender, EventArgs e)
        {
            colo.StartWithAppKey("YOUR_APP_KEY");
            colo.Delegate = this;
        }

        private void Click_Stop_Colocator(object sender, EventArgs e)
        {
            colo.Stop();
        }

        private void Click_Add_Alias(object sender, EventArgs e)
        {
            colo.AddAliasWithKey("YOUR_KEY", "YOUR_VALUE");
        }

        private void Click_Get_Device_ID(object sender, EventArgs e)
        {
            String Id = colo.DeviceId;
            outputLabel.Text = "Device ID: " + Id;
            Console.WriteLine(Id);
        }

        private void Click_Bluetooth_Perm(object sender, EventArgs e)
        {
            colo.TriggerBluetoothPermissionPopUp();
        }

        private void Click_Motion_Perm(object sender, EventArgs e)
        {
            colo.TriggerMotionPermissionPopUp();
        }

        private void Click_Integration_Test(object sender, EventArgs e)
        {
            String result = colo.TestLibraryIntegration;
            Console.WriteLine(result);
        }

        private void Click_Foreground_Service(object sender, EventArgs e)
        {
            int foregroundServiceIconID = 1;
            colo.ActivateForegroundService("YOUR_TITLE", foregroundServiceIconID, "CHANNEL");
        }

        private void Click_Get_One_Location(object sender, EventArgs e)
        {
            colo.RequestLocation();
        }

        private void Click_Register_Locations(object sender, EventArgs e)
        {
            colo.RegisterLocationListener();
        }

        private void Click_Unregister_Locations(object sender, EventArgs e)
        {
            colo.UnregisterLocationListener();
        }

        public void DidReceiveLocation(ColocatorLocationResponse location)
        {
            Console.WriteLine("Received new location " + location.ToString());

            MainThread.BeginInvokeOnMainThread(() =>
            {
                outputLabel.Text = "Lat: " + location.Latitude + " Lon: " + location.Longitude;
            });            
        }
    }
}
