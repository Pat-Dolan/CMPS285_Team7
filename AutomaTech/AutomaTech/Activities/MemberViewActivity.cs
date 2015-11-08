
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
	[Activity (Label = "MemberViewActivity")]			
	public class MemberViewActivity : Activity
	{
		GlobalVariables GMember = GlobalVariables.getInstance();
		Button btnRemove;
		Button back;
		TextView memberName;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MemberViewLayout);

			memberName = FindViewById<TextView> (Resource.Id.txtMemberViewName);
			memberName.Text = GMember.getMemberName ();

			btnRemove = FindViewById<Button> (Resource.Id.btnMemberRemove);
			btnRemove.Click += BtnRemove_Click;

			back = FindViewById<Button> (Resource.Id.btnMemberViewBack);
			back.Click += Back_Click;
		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandViewActivity));
		}

		void BtnRemove_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MemberRemoveActivity));
		}
	}
}

