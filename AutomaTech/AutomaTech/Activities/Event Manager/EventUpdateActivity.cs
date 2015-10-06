
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

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class EventUpdateActivity : Activity
	{
		GlobalVariables GEventID = GlobalVariables.getInstance();
		EditText updateTitle;
		EditText updateLocation;
		EditText updateDate;
		EditText updateTime;
		Button update;
		Button back;
		int nCount;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.EventUpdateLayout);

			updateTitle = FindViewById <EditText>(Resource.Id.txtTitle);
			updateLocation = FindViewById<EditText> (Resource.Id.txtLocation);
			updateDate = FindViewById <EditText>(Resource.Id.txtDate);
			updateTime = FindViewById <EditText>(Resource.Id.txtTime);
			update = FindViewById <Button>(Resource.Id.btnUpdate);
			update.Click += Update_Click;
			back = FindViewById<Button> (Resource.Id.btnUpdateBack);
			back.Click += Back_Click;
			update.SetText(Resource.String.UpdateEvent);

			EventDB dbr = new EventDB ();
			nCount = dbr.getEventTotal ();
			if (nCount != 0) 
			{
				SetFields ((GEventID.getEventId () ));
			}


		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity(typeof(EventMainActivity));
		}
		private void SetFields(int ID)
		{
			EventDB dbr = new EventDB ();
			var tempEvent = dbr.GetEventById ((ID + 1));

			updateTitle.Text = tempEvent.title;
			updateLocation.Text = tempEvent.location;
			updateDate.Text = tempEvent.date;
			updateTime.Text = tempEvent.time;

		}

		void Update_Click (object sender, EventArgs e)
		{
			string result;
			EventDB dbr = new EventDB ();
			result = dbr.updateEvent ((GEventID.getEventId () +1), updateTitle.Text, updateLocation.Text, updateDate.Text, updateTime.Text);
			Toast.MakeText(this, result, ToastLength.Short).Show();
		}
	}
}

