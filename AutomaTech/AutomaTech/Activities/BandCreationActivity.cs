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
	[Activity (Label = "BandCreationActivity")]			
	public class BandCreationActivity : Activity
	{
		GlobalVars g = GlobalVars.getInstance();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView(Resource.Layout.BandCreationLayout);

			//Adding layout components
			Button Finish = FindViewById<Button> (Resource.Id.finishCreation);
			Finish.Click += Finish_Click;
		}

		void Finish_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandMainActivity));
		}
	}
}