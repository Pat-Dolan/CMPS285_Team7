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
	public class EventSelectActivity : Activity
	{
		GlobalVariables GEventID = GlobalVariables.getInstance();

		private Button nView;
		private Button nUpdate;
		private Button nCancel;
		private Button nBack;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.EventSelectLayout);

			nView = FindViewById<Button> (Resource.Id.btnViewEvent);
			nView.Click += NView_Click ;

			nUpdate = FindViewById<Button> (Resource.Id.btnUpdateEvent);
			nUpdate.Click += NUpdate_Click;

			//if a member, hide the update event button
			//if(GEventID.getAccessLevel() == 0)
			//nUpdate.Visibility = ViewStates.Gone;

			nCancel = FindViewById<Button> (Resource.Id.btnRemoveEvent);
			nCancel.Click += NCancel_Click;

			//if a member, hide the cancel event button
			//if(GEventID.getAccessLevel() == 0)
			//nCancel.Visibility = ViewStates.Gone;

			nBack = FindViewById<Button> (Resource.Id.btnSelectBack);
			nBack.Click += NBack_Click;

		}

		void NBack_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(EventMainActivity));
		}

		void NCancel_Click (object sender, EventArgs e)
		{
			StartActivity(typeof(EventCancelActivity));
		}

		void NUpdate_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(EventUpdateActivity));
		}

		void NView_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(EventViewActivity));
		}
	}
}

