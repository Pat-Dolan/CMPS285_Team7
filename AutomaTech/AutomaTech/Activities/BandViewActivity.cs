
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
using System.Data;
using System.Data.SqlClient;

namespace AutomaTech
{
	[Activity (Label = "BandViewActivity")]			
	public class BandViewActivity : Activity
	{
		GlobalVariables GMemberID = GlobalVariables.getInstance();
		private List<User> nMembers;
		private ListView nMemberListView;
		int nCount;
		Button back;
		Button removeBand;
		Button setAsDefault;
		TextView bandName;
		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.BandViewLayout);

			//MemberListView
			setAsDefault = FindViewById<Button>(Resource.Id.btnSetBandDefault);
			setAsDefault.Click += SetAsDefault_Click;

			bandName = FindViewById<TextView> (Resource.Id.txtBandViewName);
			bandName.Text = GMemberID.getBandName ();

			back = FindViewById<Button> (Resource.Id.btnBackBandView);
			back.Click += Back_Click;

			removeBand = FindViewById<Button> (Resource.Id.btnRemoveBand);
			removeBand.Click += RemoveBand_Click;

			nMemberListView = FindViewById<ListView> (Resource.Id.lvMember);
			if (GMemberID.getAccessLevel () == 1) {
				nMemberListView.ItemClick += NMemberListView_ItemClick;
			}

			nMembers = new List<User>();

			UpdateMemberList ();

		}

		void RemoveBand_Click (object sender, EventArgs e)
		{
			using (SqlConnection connection = new SqlConnection(conString))
			{

				SqlCommand cmd = new SqlCommand("UPDATE bandList SET visible = @Visible WHERE id = @Id ");
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;

				cmd.Parameters.AddWithValue ("@Id", GMemberID.getBandId());
				cmd.Parameters.AddWithValue ("@Visible", 0);

				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close ();
			}
			StartActivity (typeof(BandMainActivity));
		}

		void SetAsDefault_Click (object sender, EventArgs e)
		{
			GMemberID.setDefaultBandId (GMemberID.getBandId ());
			using (SqlConnection connection = new SqlConnection(conString))
			{

				SqlCommand cmd = new SqlCommand("UPDATE userList SET defaultBandId = @DefaultBandId WHERE userId = @Id ");
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;

				cmd.Parameters.AddWithValue ("@DefaultBandId", GMemberID.getDefaultBandId());
				cmd.Parameters.AddWithValue ("@Id", GMemberID.getUserId()); 

				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close ();
			}
			string result = "Set as default";
			Toast.MakeText (this, result, ToastLength.Short).Show ();
			StartActivity (typeof(BandMainActivity));
		}

		void UpdateMemberList()
		{
			IDbConnection dbcon;
			using (dbcon = new SqlConnection (conString)) 
			{
				dbcon.Open ();
				using (IDbCommand dbcmd = dbcon.CreateCommand ()) 
				{
					string sqlGetTitle = " SELECT (userId), (userName), (access), (visible), (defaultBandId)" +
										 " FROM userList  ";
					dbcmd.CommandText = sqlGetTitle;
					using (IDataReader reader = dbcmd.ExecuteReader ()) 
					{
						nCount = 0;
						while (reader.Read ()) 
						{

							long memberUserId = (long)reader ["userId"];
							string memberName = (string)reader ["userName"];
							int access = (int)reader ["access"];
							int memberVisible = (int)reader ["visible"];
							int defaultBandId = (int)reader ["defaultBandId"];

							//Member determined by defaultBandId, determined by user if mutiple bands	
							if ((defaultBandId == GMemberID.getBandId()) && (access == 0) && (memberVisible == 1)) {
								nMembers.Add(new User(memberUserId, memberName));
							}
							nCount++;
						}
						GMemberID.setMemberTotal (nCount);
						reader.Close ();
						dbcon.Close ();
					}
				}
			}
			MemberListViewAdapter adapter = new MemberListViewAdapter (this, nMembers);
			nMemberListView.Adapter = adapter;
		}


		void NMemberListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			GMemberID.setMemberId(nMembers[e.Position].userId);
			GMemberID.setMemberName (nMembers [e.Position].userName);
			string result = " " + nMembers [e.Position].userId;
			Toast.MakeText (this, result, ToastLength.Short).Show ();
			StartActivity(typeof(MemberViewActivity));	

		}
			
		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandMainActivity));
		}
	}
}


