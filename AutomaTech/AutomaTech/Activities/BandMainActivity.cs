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
using Xamarin.Forms;
using System.Data;
using System.Data.SqlClient;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class BandMainActivity : Activity
	{
		private List<Band> nBands;
		private ListView nBandListView;
		Button newBand;
		Button back;
		int nCount;
		GlobalVariables GBand = GlobalVariables.getInstance();
		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView(Resource.Layout.BandMainLayout);

			//Adding components

			newBand = FindViewById<Button> (Resource.Id.btnNewBand);
			//if user is a member, remove the new band button
			if (GBand.getAccessLevel() == 0)
				newBand.Visibility = ViewStates.Gone;
			newBand.Click += NewBand_Click;
			//Test if any bands managed

			back = FindViewById<Button> (Resource.Id.btnBandMainBack);
			back.Click += Back_Click;
		
			nBandListView = FindViewById<ListView> (Resource.Id.lvBand);
			nBandListView.ItemClick += NBandListView_ItemClick;
			nBands = new List<Band>();

			UpdateBandList ();

		}

		void UpdateBandList()
		{
			IDbConnection dbcon;
			using (dbcon = new SqlConnection (conString)) 
			{
				dbcon.Open ();
				using (IDbCommand dbcmd = dbcon.CreateCommand ()) 
				{
					string sqlGetTitle = " SELECT (id),(bandName),(managerId),(visible) " +
						" FROM bandList ";
					dbcmd.CommandText = sqlGetTitle;
					using (IDataReader reader = dbcmd.ExecuteReader ()) 
					{
						nCount = 0;
						while (reader.Read ()) 
						{

							int bandId = (int)reader ["id"];
							string bandName = (string)reader ["bandName"];
							long bandManager = (long)reader ["managerId"];
							int bandVisible = (int)reader ["visible"];
							//for cancel	
							if ((bandVisible == 1) && (bandManager == GBand.getManagerId())) {
								if(GBand.getAccessLevel() == 1)
									nBands.Add(new Band(bandId, bandName, bandManager, bandVisible));
								//if the user is a member, display only default band
								else if(bandId == GBand.getDefaultBandId())
									nBands.Add(new Band(bandId, bandName, bandManager, bandVisible));
							}
							nCount++;
						}
						GBand.setBandTotal (nCount);
						reader.Close ();
						dbcon.Close ();
					}
				}
			}
			BandListViewAdapter adapter = new BandListViewAdapter (this, nBands);
			nBandListView.Adapter = adapter;
		}


		void NBandListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			GBand.setBandId(nBands[e.Position].bandId);
			GBand.setBandName (nBands [e.Position].bandName);
			StartActivity(typeof(BandViewActivity));
		}
			

		void NewBand_Click (object sender, EventArgs e)
		{
			GBand.setBandId((GBand.getBandTotal() + 1));
			StartActivity (typeof(BandCreationActivity));
		}
			

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MainActivity));
		}
	}
}

