using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
namespace AutomaTech
{
	//This list view adapter builds the band list
	public class BandListViewAdapter : BaseAdapter<Band>
	{
		GlobalVariables GBandID = GlobalVariables.getInstance();
		public List<Band> nBands;
		private Context nBandContext;

		public BandListViewAdapter(Context context, List<Band> bands)
		{
			nBands = bands;
			nBandContext = context;
		}
		public override int Count {
			get 
			{
				return nBands.Count ();
			}
		}

		public override long GetItemId (int position)
		{
			return position;
		}
		public override Band this[int position] {
			get 
			{
				return nBands [position];
			}
		}
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null) 
			{
				row = LayoutInflater.From (nBandContext).Inflate (Resource.Layout.BandListViewRowLayout, null, false);
			}

			TextView txtBand = row.FindViewById<TextView> (Resource.Id.lvBandName);
			txtBand.Text = nBands [position].bandName;

			
			

			return row;

		}
	}
}
