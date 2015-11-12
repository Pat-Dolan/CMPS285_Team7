
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Data;
using System.Data.SqlClient;
using Xamarin.Social;
using Xamarin.Social.Services;
using Xamarin.Auth;
using Xamarin.Media;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class EventViewActivity : Activity

	{
		private static TwitterService mTwitter;

		GlobalVariables GEventID = GlobalVariables.getInstance();
		private TextView nTitle;
		private TextView nLocation;
		private TextView nDate;
		private TextView nTime;
		private Button nBack;
		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.EventViewLayout);
			nTitle = FindViewById<TextView> (Resource.Id.txtTitle);
			nLocation = FindViewById<TextView> (Resource.Id.txtLocation);
			nDate = FindViewById<TextView> (Resource.Id.txtDate);
			nTime = FindViewById<TextView> (Resource.Id.txtTime);

			nBack = FindViewById<Button> (Resource.Id.btnViewBack);
			nBack.Click += NBack_Click;

			SetFields (GEventID.getEventId());

			Button twitter = FindViewById<Button> (Resource.Id.TwitterShare);
			twitter.Click += (object sender, EventArgs e) => {
				try {
					Sharing (Twitter, twitter);
				} catch (Exception ex) {
					ShowMessage ("Twitter: " + ex.Message);
				}
			};
		}

		public static TwitterService Twitter
		{
			get
			{
				if (mTwitter == null)
				{
					mTwitter = new TwitterService {
						ConsumerKey = "ERMyIfX25iuUtKmmk2CRoSJFA",
						ConsumerSecret = "smCmzT6KpFBetryvpfGE7fvu0WmVdY1sRhJxqV7gwOIweFPs3d",
						CallbackUrl = new Uri ("http://mobile.twitter.com")
					};
				}
				return mTwitter;
			}
		}
		void Sharing (Xamarin.Social.Service service, Button shareButton)
		{
			string tweetString = nTitle.Text.Trim() + "\nAt: " + nLocation.Text.Trim() + "\nTime: " + nTime.Text.Trim() + "\nDate: " + nDate.Text.Trim();
			Item item = new Item {
				Text = tweetString
			};

			Intent intent = service.GetShareUI (this, item, shareResult => {
				string tweetResult = "Tweet Successful";
				Toast.MakeText(this, tweetResult, ToastLength.Short).Show();
			});

			StartActivity (intent);
		}

		private void ShowMessage(String message)
		{
			Toast.MakeText(this, message, ToastLength.Long).Show();

		}

		void SetFields(int ID)
		{

			//SQLite Database
			//			EventDB dbr = new EventDB ();
			//			var selectedEvent = dbr.GetEventById ((GEventID.getEventId ()));
			//				nTitle.Text = selectedEvent.title;
			//				nLocation.Text = selectedEvent.location;
			//				nDate.Text = selectedEvent.date;
			//				nTime.Text = selectedEvent.time;

			bool found = false;
			int eventId;
			IDbConnection dbcon;
			using (dbcon = new SqlConnection (conString)) 
			{
				dbcon.Open ();
				using (IDbCommand dbcmd = dbcon.CreateCommand ()) 
				{
					string sqlGetEventInfo = " SELECT (id), (title), (location), (date), (time) FROM EventList ";

					dbcmd.CommandText = sqlGetEventInfo;
					using (IDataReader reader = dbcmd.ExecuteReader ()) 
					{
						while (reader.Read() && (found == false)) 
						{
							eventId = (int)reader ["id"];

							nTitle.Text = (string)reader ["title"];
							nLocation.Text = (string)reader ["location"];
							nDate.Text = (string)reader ["date"];
							nTime.Text = (string)reader ["time"];
							if(eventId == ID)
								found = true;
						}
						reader.Close ();
						dbcon.Close ();
					}
				}
			}
		}
		protected async override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (requestCode != 1)
				return;

			if (resultCode == Result.Canceled)
				return;

			var file = await data.GetMediaFileExtraAsync (this);

			try
			{
				using (var stream = file.GetStream ()) {
					var item = new Item (" ") {
						Images = new[] { new ImageData (file.Path) }
					};

					Intent intent = Twitter.GetShareUI (this, item, shareResult => {
						FindViewById<Button> (Resource.Id.TwitterShare).Text = "" + shareResult;
					});

					StartActivity (intent);
				}
			}
			catch (Exception ex)
			{
				ShowMessage(" " + ex.Message);
			}
		}
		void NBack_Click (object sender, EventArgs e)
		{
			if (GEventID.getAccessLevel () == 1)
				StartActivity (typeof(EventSelectActivity));
			else
				StartActivity (typeof(EventMainActivity));
		}


	}
}

