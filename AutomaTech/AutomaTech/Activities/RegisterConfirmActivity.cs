
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

			//Setting layout
			SetContentView(Resource.Layout.RegisterConfirmationLayout);

			//Adding components
			Button confirm = FindViewById<Button> (Resource.Id.btnConfirmCode);
			confirm.Click += Confirm_Click;

			Button returnReg = FindViewById<Button> (Resource.Id.btnRegisterScreen);
			returnReg.Click += ReturnReg_Click;
		}

		//This handler returns the user to the registration page to correct information. 
		//*****THIS BUTTON CURRENTLY RESUBMITS USER INFORMATION!!!!*******
		void ReturnReg_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(RegisterMainActivity));
		}

		void Confirm_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(RegisterFinalizeActivity));
		}
	}
}

