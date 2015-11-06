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
					string sqlGetTitle = " SELECT (bandId),(bandName),(bandManager),(bandVisible) " +
						" FROM bandList ";
					dbcmd.CommandText = sqlGetTitle;
					using (IDataReader reader = dbcmd.ExecuteReader ()) 
					{
						nCount = 0;
						while (reader.Read ()) 
						{

							int bandId = (int)reader ["bandId"];
							string bandName = (string)reader ["bandName"];
							string bandManager = (string)reader ["bandManager"];
							int bandVisible = (int)reader ["bandVisible"];
							//for cancel	
							if (bandVisible == 1) {
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
			string result = " " + nBands [e.Position].bandId;
			Toast.MakeText (this, result, ToastLength.Short).Show ();
			StartActivity(typeof(BandViewActivity));

		}
			

		void NewBand_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandCreationActivity));
		}
			

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MainActivity));
		}
	}
}

