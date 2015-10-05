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
	[Activity (Label = "TourPlus+", Icon = "@drawable/icon")]			
	public class EventMainActivity : Activity
	{
		GlobalVars g = GlobalVars.getInstance();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView(Resource.Layout.EventMainLayout);

			//Adding components
			Button newEvent = FindViewById<Button> (Resource.Id.btnNewEvent);
			newEvent.Click+= NewEvent_Click;

			Button updateEvent = FindViewById<Button> (Resource.Id.btnUpdateEvent);
			updateEvent.Click += UpdateEvent_Click;


			Button viewEvents = FindViewById<Button> (Resource.Id.btnGetEvents);
			viewEvents.Click += ViewEvents_Click; 

			Button homeFromEvent = FindViewById<Button> (Resource.Id.homeFromEvent);
			homeFromEvent.Click += HomeFromEvent_Click;
		}

		void ViewEvents_Click (object sender, EventArgs e)
		{
			int LogId = g.getTest ();
			string showing = LogId.ToString ();
			Toast.MakeText (this, showing, ToastLength.Short).Show ();
		}

		void RemoveEvent_Click (object sender, EventArgs e)
		{
			
		}

		void UpdateEvent_Click (object sender, EventArgs e)
		{
			
		}

		void NewEvent_Click (object sender, EventArgs e)
		{
			
		}
		void HomeFromEvent_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MainActivity));
		}
	}
}

