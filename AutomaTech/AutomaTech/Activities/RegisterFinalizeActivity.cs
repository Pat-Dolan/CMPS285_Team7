
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
	[Activity (Label = "RegisterFinalizeActivity")]			
	public class RegisterFinalizeActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.RegisterFinalizeLayout);
			Button finishReg = FindViewById<Button> (Resource.Id.btnFinalize);
			finishReg.Click += FinishReg_Click;
		}

		void FinishReg_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(LoginActivity));
		}
	}
}

