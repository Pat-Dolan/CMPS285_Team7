
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
	[Activity (Label = "NewMemberMainActivity")]			
	public class NewMemberMainActivity : Activity
	{
		GlobalVariables GMember = GlobalVariables.getInstance();
		Button addNewMember;
		Button back;
		Button finish;
		private List<User> nMembers;
		private ListView nMemberListView;
		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.AddMemberLayout);

			addNewMember = FindViewById<Button> (Resource.Id.btnNewMember);
			addNewMember.Click += AddNewMember_Click;

			finish = FindViewById<Button> (Resource.Id.btnFinishCreation);
			finish.Click += Finish_Click;

			back = FindViewById<Button> (Resource.Id.btnNewMemberBack);
			back.Click += Back_Click;

			nMemberListView = FindViewById<ListView> (Resource.Id.lvAddMember);
			nMemberListView.ItemClick += NMemberListView_ItemClick;
			nMembers = new List<User>();

			UpdateMemberList ();
			// Create your application here
		}
		void NMemberListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			GMember.setMemberId(nMembers[e.Position].userId);
			GMember.setMemberName (nMembers [e.Position].userName);
			string result = " " + nMembers [e.Position].userId;
			Toast.MakeText (this, result, ToastLength.Short).Show ();
			StartActivity(typeof(MemberNewViewActivity));	

		}
		void Finish_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandMainActivity));
		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandMainActivity));
		}

		void AddNewMember_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MemberNewActivity));
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
						while (reader.Read ()) 
						{

							long memberUserId = (long)reader ["userId"];
							string memberName = (string)reader ["userName"];
							int access = (int)reader ["access"];
							int memberVisible = (int)reader ["visible"];
							int defaultBandId = (int)reader ["defaultBandId"];

							//Member determined by defaultBandId, determined by user if mutiple bands	
							if ((defaultBandId == GMember.getBandId()) && (access == 0) && (memberVisible == 1)) {
								nMembers.Add(new User(memberUserId, memberName));
							}
						
						}

						reader.Close ();
						dbcon.Close ();
					}
				}
			}
			MemberListViewAdapter adapter = new MemberListViewAdapter (this, nMembers);
			nMemberListView.Adapter = adapter;
		}

	}
}

