
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
using SQLite;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class EventNewActivity : Activity
	{
		GlobalVariables GEventID = GlobalVariables.getInstance();
		EditText newTitle;
		EditText newLocation;
		EditText newDate;
		EditText newTime;
		Button newEvent;
		Button back;
		int nCount;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.EventUpdateLayout);

			newTitle = FindViewById <EditText> (Resource.Id.txtTitle);
			newLocation = FindViewById<EditText> (Resource.Id.txtLocation);
			newDate = FindViewById <EditText> (Resource.Id.txtDate);
			newTime = FindViewById <EditText> (Resource.Id.txtTime);
			newEvent = FindViewById <Button> (Resource.Id.btnUpdate);
			newEvent.Click += new_Click;

			newEvent.SetText(Resource.String.NewEvent);
			back = FindViewById<Button> (Resource.Id.btnUpdateBack);
			back.Click += Back_Click;

			EventDB dbr = new EventDB ();
			nCount = dbr.getEventTotal ();
	
		}
		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(EventMainActivity));
		
		}
			
		void new_Click (object sender, EventArgs e)
		{
			string result;
			EventDB dbr = new EventDB ();
			result = dbr.InsertEvent (newTitle.Text, newLocation.Text, newDate.Text, newTime.Text);
			Toast.MakeText(this, result, ToastLength.Short).Show();
		}
	}
}

