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
using System.Data;
using System.Data.SqlClient;

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

		private TextView dateDisplay;
		private Button pickDate;
		private DateTime date;
		private TextView timeDisplay;

		private int hour;
		private int minute;
		const int TIME_DIALOG_ID = 0;
		const int DATE_DIALOG_ID = 1;


		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.EventUpdateLayout);

			timeDisplay = FindViewById<TextView> (Resource.Id.txtTime);

			// Add a click listener to the button
			TextView timePick = FindViewById<TextView>(Resource.Id.txtTime);
			timePick.Click += (o, e) => ShowDialog (TIME_DIALOG_ID);

			// Get the current time
			hour = DateTime.Now.Hour;
			minute = DateTime.Now.Minute;

			dateDisplay = FindViewById<TextView> (Resource.Id.txtDate);
			pickDate = FindViewById<Button> (Resource.Id.btnUpdateDate);

			// add a click event handler to the button
			TextView datePick = FindViewById<TextView>(Resource.Id.txtDate);
			datePick.Click += delegate { ShowDialog (DATE_DIALOG_ID); };

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

			SetFields (GEventID.getEventId());
		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity(typeof(EventMainActivity));
		}

		private void SetFields(int ID)
		{
			
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

							updateTitle.Text = (string)reader ["title"];
							updateLocation.Text = (string)reader ["location"];
							dateDisplay.Text = (string)reader ["date"];
							timeDisplay.Text = (string)reader ["time"];
							if(eventId == ID)
								found = true;
						}
						reader.Close ();
						dbcon.Close ();
					}
				}
			}
		}
			
		void Update_Click (object sender, EventArgs e)
		{
			//SQL Server Database Tools
			using (SqlConnection connection = new SqlConnection(conString))
			{
				SqlCommand cmd = new SqlCommand("UPDATE EventList SET title = @Title, location = @Location, date = @Date, time = @Time WHERE id = @Id ");
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;

				cmd.Parameters.AddWithValue ("@Id", GEventID.getEventId());
				cmd.Parameters.AddWithValue("@Title", updateTitle.Text);
				cmd.Parameters.AddWithValue ("@Location", updateLocation.Text);
				cmd.Parameters.AddWithValue ("@Date", dateDisplay.Text);
				cmd.Parameters.AddWithValue ("@Time", timeDisplay.Text); 

				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close ();
			}

			StartActivity (typeof(EventMainActivity));
		}
			
		private void UpdateDisplay ()
		{
			string time = getMidiTime ();
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
			switch (id) 
			{
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

