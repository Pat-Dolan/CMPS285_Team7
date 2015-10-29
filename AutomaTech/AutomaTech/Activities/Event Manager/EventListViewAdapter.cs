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
	public class EventListViewAdapter : BaseAdapter<Event>
	{
		GlobalVariables GEventID = GlobalVariables.getInstance();
		public List<Event> nEvents;
		private Context nEventContext;

		public EventListViewAdapter(Context context, List<Event> events)
		{
			nEvents = events;
			nEventContext = context;
		}
		public override int Count {
			get 
			{
				return nEvents.Count ();
			}
		}

		public override long GetItemId (int position)
		{
			return position;
		}
		public override Event this[int position] {
			get 
			{
				return nEvents [position];
			}
		}
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null) 
			{
				row = LayoutInflater.From (nEventContext).Inflate (Resource.Layout.EventListViewRowLayout, null, false);
			}

			TextView txtEvent = row.FindViewById<TextView> (Resource.Id.txtEventTitle);
			txtEvent.Text = nEvents [position].eventTitle;
			TextView txtEDate = row.FindViewById<TextView> (Resource.Id.txtEventDate);
			txtEDate.Text = nEvents [position].eventDate;

			return row;

		}
	}
}

