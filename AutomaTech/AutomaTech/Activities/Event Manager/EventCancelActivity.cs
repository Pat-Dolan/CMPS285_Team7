
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
	public class EventCancelActivity : Activity
	{
		GlobalVariables GEventID = GlobalVariables.getInstance();
		Button eventCancel;
		Button back;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.EventCancelLayout);
			eventCancel = FindViewById<Button> (Resource.Id.btnCancel);
			eventCancel.Click += EventCancel_Click;
			//TextView title = FindViewById<TextView> (Resource.Id.txtCancelTitle);
	
			back = FindViewById<Button> (Resource.Id.btnCancelBack);
			back.Click += Back_Click;
	
			//EventDB dbr = new EventDB ();
			//var item = dbr.GetEventById (GEventID.getEventId () + 1);


		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(EventSelectActivity));
		}

		void EventCancel_Click (object sender, EventArgs e)
		{
			string result = "Remove here";
			Toast.MakeText (this, result, ToastLength.Short).Show ();
		}


	}
}

