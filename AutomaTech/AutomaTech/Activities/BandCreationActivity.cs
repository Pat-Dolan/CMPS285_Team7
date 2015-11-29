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
using System.Data.SqlClient;
using System.Data;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class BandCreationActivity : Activity
	{
		GlobalVariables GBand = GlobalVariables.getInstance();
		EditText bandName;
		Button newBand;
		Button back;

		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView(Resource.Layout.BandCreationLayout);

			bandName = FindViewById<EditText>(Resource.Id.txtNewBandName);

			back = FindViewById<Button> (Resource.Id.btnBackBandNew);
			back.Click += Back_Click;

			newBand = FindViewById<Button> (Resource.Id.btnNewBandNext);
			newBand.Click += NewBand_Click;
		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandMainActivity));
		}
		void NewBand_Click (object sender, EventArgs e)
		{
			if (bandName.Text != "") 
			{
				GBand.setBandName (bandName.Text);
				StartActivity (typeof(NewMemberMainActivity));
			} 
			else 
			{
				string tip = "Enter a Band Name to continue";
				Toast.MakeText (this, tip, ToastLength.Short).Show ();
			}
		}
	}
}