
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Social;
using Xamarin.Social.Services;
using Xamarin.Auth;
using System.Threading.Tasks;
using Xamarin.Media;

namespace Login
{
	[Activity (Label = "Twitter")]			
	public class MainActivity : Activity
	{
		private static TwitterService mTwitter;
		public static TwitterService Twitter
		{
			get
			{
				if (mTwitter == null)
				{
					mTwitter = new TwitterService {
						ConsumerKey = "ERMyIfX25iuUtKmmk2CRoSJFA",
						ConsumerSecret = "smCmzT6KpFBetryvpfGE7fvu0WmVdY1sRhJxqV7gwOIweFPs3d",
						CallbackUrl = new Uri ("http://www.twitter.com")
					};
				}

				return mTwitter;
			}
		}

		void Sharing (Xamarin.Social.Service service, Button shareButton)
		{
			Item item = new Item {
				Text = "I'm sharing great things using Xamarin!"
			};

			Intent intent = service.GetShareUI (this, item, shareResult => {
				shareButton.Text = service.Title + " shared: " + shareResult;
			});

			StartActivity (intent);
		}

		private void ShowMessage(String message)
		{
			Toast.MakeText(this, message, ToastLength.Long).Show();

		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			Button twitterButton = FindViewById<Button> (Resource.Id.Twitter);
			twitterButton.Click += (object sender, EventArgs e) => {
				try {
					Sharing (Twitter, twitterButton);
				} catch (Exception ex) {
					ShowMessage ("Twitter: " + ex.Message);
				}
			};
		}
	}
}

