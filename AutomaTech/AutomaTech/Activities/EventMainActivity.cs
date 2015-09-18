
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
	[Activity (Label = "EventMainActivity", Icon = "@drawable/icon")]			
	public class EventMainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.EventMainLayout);

			Button newEvent = FindViewById<Button> (Resource.Id.btnNewEvent);
			newEvent.Click+= NewEvent_Click;

			Button updateEvent = FindViewById<Button> (Resource.Id.btnUpdateEvent);
			updateEvent.Click += UpdateEvent_Click;

			Button removeEvent = FindViewById<Button> (Resource.Id.btnRemoveEvent);
			removeEvent.Click += RemoveEvent_Click; 

			Button viewEvents = FindViewById<Button> (Resource.Id.btnGetEvents);
			viewEvents.Click += ViewEvents_Click; 
		}

		void ViewEvents_Click (object sender, EventArgs e)
		{
			
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
	}
}

