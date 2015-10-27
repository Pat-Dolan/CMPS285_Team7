
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
		Button newEvent;
		Button back;
		int nCount;

		private TextView timeDisplay;
		private Button pick_button;

		private int hour;
		private int minute;

		const int TIME_DIALOG_ID = 0;

		private TextView dateDisplay;
		private Button pickDate;
		private DateTime date;

		const int DATE_DIALOG_ID = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.EventUpdateLayout);

			timeDisplay = FindViewById<TextView> (Resource.Id.txtTime);
			//pick_button = FindViewById<Button> (Resource.Id.btnUpdateTime);

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


			newTitle = FindViewById <EditText> (Resource.Id.txtTitle);
			newLocation = FindViewById<EditText> (Resource.Id.txtLocation);
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
			result = dbr.InsertEvent (newTitle.Text, newLocation.Text, dateDisplay.Text, timeDisplay.Text);
			StartActivity (typeof(EventMainActivity));
			//Toast.MakeText(this, result, ToastLength.Short).Show();
		}
		//MIDI time
		private void UpdateDisplay ()
		{
			string time = getMidiTime();
			timeDisplay.Text = time;
			dateDisplay.Text = date.ToString ("d");
		}
		private string getMidiTime()
		{
			string postfix;		//holds am or pm

			//testing for pm or am
			if (hour < 12)
				postfix = " am";
			else
				postfix = " pm";

			//converting military time to standard time
			int tester = hour % 12;
			if (tester == 0)
				hour = 12;
			else
				hour = tester;
			return hour.ToString() + ":" + minute.ToString().PadLeft (2, '0') + postfix;

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

