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
	[Activity (Label = "BandMainActivity", Icon = "@drawable/icon")]			
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

			Button removeBand = FindViewById<Button> (Resource.Id.btnRemoveBand);
			removeBand.Click += RemoveBand_Click;

			Button getBands = FindViewById<Button> (Resource.Id.btnGetBands);
			getBands.Click += GetBands_Click;
		}

		void GetBands_Click (object sender, EventArgs e)
		{
			
		}

		void RemoveBand_Click (object sender, EventArgs e)
		{
			
		}

		void NewBand_Click (object sender, EventArgs e)
		{
			
		}

		void UpdateBand_Click (object sender, EventArgs e)
		{
			
		}
	}
}

