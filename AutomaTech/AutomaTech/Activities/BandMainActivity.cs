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
using Xamarin.Forms;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class BandMainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView(Resource.Layout.BandMainLayout);

			//Adding components
			Button newBand = FindViewById<Button> (Resource.Id.btnNewBand);
			newBand.Click += NewBand_Click;

			Button updateBand = FindViewById<Button> (Resource.Id.btnUpdateBand);
			updateBand.Click += UpdateBand_Click;

			Button getBands = FindViewById<Button> (Resource.Id.btnGetBands);
			getBands.Click += GetBands_Click;

			Button removeBand = FindViewById<Button> (Resource.Id.btnRemoveBand);
			removeBand.Click += RemoveBand_Click;

			Button homeFromBand = FindViewById<Button> (Resource.Id.homeFromBand);
			homeFromBand.Click += HomeFromBand_Click;
		}


		void NewBand_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandCreationActivity));
		}
			
		void UpdateBand_Click (object sender, EventArgs e)
		{
			//AlertDialog ("Notice", "You do not have a band set up!", "Ok");
		}

		void GetBands_Click (object sender, EventArgs e)
		{
			//AlertDialog dialog ("Notice", "You do not have a band set up!", "Ok");
		}
			
		void RemoveBand_Click (object sender, EventArgs e)
		{
			//AlertDialog dialog ("Notice", "You do not have a band set up!", "Ok");
		}
		void HomeFromBand_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MainActivity));
		}
	}
}

