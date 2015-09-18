
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
	[Activity (Label = "RegisterMainActivity")]			
	public class RegisterMainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.RegisterMainLayout);
			Button register = FindViewById<Button>(Resource.Id.btnRegister);
			register.Click += Register_Click;
		}

		void Register_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(RegisterConfirmActivity));
		}
	}
}

