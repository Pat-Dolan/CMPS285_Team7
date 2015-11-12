using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]
	public class EventMainActivity : Activity
	{

		GlobalVariables GEventID = GlobalVariables.getInstance();	//Should be used to get username from Facebook

		int nCount = 0;
		private List<Event> nEvents;	
		private ListView nEventListView;

		Button firstEventbtn;

		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.EventMainLayout);

			firstEventbtn = FindViewById<Button> (Resource.Id.btnFirstEvent);
			firstEventbtn.Click += FirstEventbtn_Click;

			//if user is a member, hide the new event button
			if(GEventID.getAccessLevel() == 0)
			firstEventbtn.Visibility = ViewStates.Gone;

			Button back = FindViewById<Button> (Resource.Id.btnEventMainBack);
			back.Click += Back_Click;

			nEventListView = FindViewById<ListView> (Resource.Id.lvEvent);
			nEventListView.ItemClick += NEventListView_ItemClick;

			nEvents = new List<Event>();

			UpdateEventList ();
		}

		void UpdateEventList ()
		{
			//getting titles for initial event list view
			IDbConnection dbcon;
			using (dbcon = new SqlConnection (conString)) 
			{
				dbcon.Open ();
				using (IDbCommand dbcmd = dbcon.CreateCommand ()) 
				{
					string sqlGetTitle = " SELECT (id),(title),(date),(time), (visible), (bandId) " +
						" FROM EventList ";
					dbcmd.CommandText = sqlGetTitle;
					using (IDataReader reader = dbcmd.ExecuteReader ()) 
					{
						nCount = 0;
						while (reader.Read ()) 
						{

							int eventId = (int)reader ["id"];
							string eventTitle = (string)reader ["title"];
							string eventDate = (string)reader ["date"];
							int eventVisible = (int)reader ["visible"];
							int eventBandId = (int)reader ["bandId"];
								
							if ((eventVisible == 1)&& (eventBandId == GEventID.getDefaultBandId())) {
								nEvents.Add (new Event ((eventId), eventTitle, eventDate, eventVisible));
							}

							
							nCount++;
						}
						GEventID.setEventTotal (nCount);
						reader.Close ();
						dbcon.Close ();
					}
				}
			}

			EventListViewAdapter adapter = new EventListViewAdapter (this, nEvents);
			nEventListView.Adapter = adapter;
		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandViewActivity));
		}

		void NEventListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			GEventID.setEventId(nEvents[e.Position].eventId);
			if (GEventID.getAccessLevel () == 1)
				StartActivity (typeof(EventSelectActivity));
			else
				StartActivity (typeof(EventViewActivity));
		}

		void FirstEventbtn_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(EventNewActivity));
		}
	}
}



