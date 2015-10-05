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
	[Activity (Label = "TourPlus+", Icon = "@drawable/icon")]			
	public class TourMainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView(Resource.Layout.TourMainLayout);
		}
	}
}

