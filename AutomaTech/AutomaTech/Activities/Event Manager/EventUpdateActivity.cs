
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
		Button update;
		Button back;
		int nCount;

		private TextView timeDisplay;

		private int hour;
		private int minute;

		const int TIME_DIALOG_ID = 0;

		private TextView dateDisplay;
		private DateTime date;

		const int DATE_DIALOG_ID = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.EventUpdateLayout);


			timeDisplay = FindViewById<TextView> (Resource.Id.txtTime);

			// Add a click listener to the button
			timeDisplay.Click += (o, e) => ShowDialog (TIME_DIALOG_ID);

			// Get the current time
			hour = DateTime.Now.Hour;
			minute = DateTime.Now.Minute;

			dateDisplay = FindViewById<TextView> (Resource.Id.txtDate);

			// add a click event handler to the button
			dateDisplay.Click += delegate { ShowDialog (DATE_DIALOG_ID); };

			// get the current date
			date = DateTime.Today;

			// Display the current date/time
			UpdateDisplay ();

			updateTitle = FindViewById <EditText>(Resource.Id.txtTitle);
			updateLocation = FindViewById<EditText> (Resource.Id.txtLocation);
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
			dateDisplay.Text = tempEvent.date;
			timeDisplay.Text = tempEvent.time;

		}

		void Update_Click (object sender, EventArgs e)
		{
			string result;
			EventDB dbr = new EventDB ();
			result = dbr.updateEvent ((GEventID.getEventId () +1), updateTitle.Text, updateLocation.Text, dateDisplay.Text, timeDisplay.Text);
			Toast.MakeText(this, result, ToastLength.Short).Show();
		}
		private void UpdateDisplay ()
		{
			string time = string.Format ("{0}:{1}", hour, minute.ToString ().PadLeft (2, '0'));
			timeDisplay.Text = time;
			dateDisplay.Text = date.ToString ("d");
		}
		private void TimePickerCallback (object sender, TimePickerDialog.TimeSetEventArgs e)
		{
			hour = e.HourOfDay;
			minute = e.Minute;
			UpdateDisplay ();
		}
		protected override Dialog OnCreateDialog (int id)
		{
			switch (id) {
			case DATE_DIALOG_ID:
				return new DatePickerDialog (this, OnDateSet, date.Year, date.Month - 1, date.Day); 

			case TIME_DIALOG_ID:
				return new TimePickerDialog (this, TimePickerCallback, hour, minute, false);
			}
			return null;
		}
		void OnDateSet (object sender, DatePickerDialog.DateSetEventArgs e)
		{
			this.date = e.Date;
			UpdateDisplay ();
		}
	}
}

