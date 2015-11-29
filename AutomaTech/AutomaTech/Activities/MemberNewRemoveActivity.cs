
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
using System.Data.SqlClient;
using System.Data;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class MemberNewRemoveActivity : Activity
	{
		GlobalVariables GMember = GlobalVariables.getInstance();

		Button removeMember;
		Button back;
		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.EventCancelLayout);
			removeMember = FindViewById<Button> (Resource.Id.btnCancel);
			removeMember.Click += RemoveMember_Click;
			removeMember.SetText (Resource.String.RemoveMember);

			back = FindViewById<Button> (Resource.Id.btnCancelBack);
			back.Click += Back_Click;
		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MemberNewViewActivity));
		}

		void RemoveMember_Click (object sender, EventArgs e)
		{
			using (SqlConnection connection = new SqlConnection(conString))
			{

				SqlCommand cmd = new SqlCommand("UPDATE userList SET visible = @Visible WHERE userId = @Id ");
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;

				cmd.Parameters.AddWithValue ("@Id", GMember.getMemberId());
				cmd.Parameters.AddWithValue ("@Visible", 0);

				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close ();
			}
			StartActivity (typeof(NewMemberMainActivity));
		}
	}
}

