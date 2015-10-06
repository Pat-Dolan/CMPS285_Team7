using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]
	public class EventMainActivity : Activity
	{
		
		GlobalVariables GEventID = GlobalVariables.getInstance();
		int nCount;
		private List<Event> nEvents;
		private ListView nEventListView;

		Button firstEventbtn;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.EventMainLayout);
			firstEventbtn = FindViewById<Button> (Resource.Id.btnFirstEvent);
			firstEventbtn.Click += FirstEventbtn_Click;

			Button back = FindViewById<Button> (Resource.Id.btnEventMainBack);
			back.Click += Back_Click;

			nEventListView = FindViewById<ListView> (Resource.Id.lvEvent);

			nEvents = new List<Event>();

			EventDB dbr = new EventDB ();
			var result = dbr.CreateDB ();
			nCount = dbr.getEventTotal ();

			//Building the event table
			if (nCount != 0) {
				for (int i = 1; i <= nCount; i++) {
					var nextEvent = dbr.GetEventById (i);
					nEvents.Add (new Event (nextEvent.title, nextEvent.date));
				}
			}
			EventListViewAdapter adapter = new EventListViewAdapter (this, nEvents);

			nEventListView.Adapter = adapter;
			nEventListView.ItemClick += NEventListView_ItemClick;
		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MainActivity));
		}

	

		void NEventListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			GEventID.setEventId(e.Position);
			StartActivity (typeof(EventSelectActivity));
		}

		void FirstEventbtn_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(EventNewActivity));
		}
	}
}



