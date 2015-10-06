
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

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class EventViewActivity : Activity

	{
		GlobalVariables GEventID = GlobalVariables.getInstance();
		private TextView nTitle;
		private TextView nLocation;
		private TextView nDate;
		private TextView nTime;
		private Button nBack;
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

			SetFields ();

		}

		void SetFields()
		{
			
				EventDB dbr = new EventDB ();
			var selectedEvent = dbr.GetEventById ((GEventID.getEventId ()+1));
				nTitle.Text = selectedEvent.title;
				nLocation.Text = selectedEvent.location;
				nDate.Text = selectedEvent.date;
				nTime.Text = selectedEvent.time;

		}

		void NBack_Click (object sender, EventArgs e)
		{
			StartActivity(typeof(EventSelectActivity));
		}


	}
}

