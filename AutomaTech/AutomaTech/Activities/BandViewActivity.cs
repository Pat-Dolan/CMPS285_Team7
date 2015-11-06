
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
	[Activity (Label = "BandViewActivity")]			
	public class BandViewActivity : Activity
	{
		Button back;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.BandViewLayout);

			back = FindViewById<Button> (Resource.Id.btnBackBandView);
			back.Click += Back_Click;


		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandMainActivity));
		}
	}
}

