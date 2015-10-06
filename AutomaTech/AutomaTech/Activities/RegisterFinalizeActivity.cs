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
	public class RegisterFinalizeActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView(Resource.Layout.RegisterFinalizeLayout);

			//Adding components
			Button finishReg = FindViewById<Button> (Resource.Id.btnFinalize);
			finishReg.Click += FinishReg_Click;
		}

		void FinishReg_Click (object sender, EventArgs e)
		{
			StartActivity(typeof(LoginActivity));
		}
	}
}

