
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
	[Activity (Label = "RegisterConfirmActivity")]			
	public class RegisterConfirmActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.RegisterConfirmationLayout);
			Button confirm = FindViewById<Button> (Resource.Id.btnConfirmCode);
			confirm.Click += Confirm_Click;



			Button returnReg = FindViewById<Button> (Resource.Id.btnRegisterScreen);
			returnReg.Click += ReturnReg_Click;
		}

		void ReturnReg_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(RegisterMainActivity));
		}

		void Confirm_Click (object sender, EventArgs e)
		{
			//if confirmed, start finalize

			//TESTING DB TABLE
			//DBRepository dbr = new DBRepository();
			//string result = dbr.GetAccount ();
			//Toast.MakeText (this, result, ToastLength.Short).Show ();


			StartActivity (typeof(RegisterFinalizeActivity));
		}
	}
}

