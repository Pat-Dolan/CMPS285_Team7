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
	public class MemberListViewAdapter : BaseAdapter<User>
	{
		GlobalVariables GMemberID = GlobalVariables.getInstance();
		public List<User> nMembers;
		private Context nMemberContext;

		public MemberListViewAdapter(Context context, List<User> members)
		{
			nMembers = members;
			nMemberContext = context;
		}
		public override int Count {
			get 
			{
				return nMembers.Count ();
			}
		}

		public override long GetItemId (int position)
		{
			return position;
		}
		public override User this[int position] {
			get 
			{
				return nMembers [position];
			}
		}
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null) 
			{
				row = LayoutInflater.From (nMemberContext).Inflate (Resource.Layout.MemberListViewRowLayout, null, false);
			}

			TextView memberName = row.FindViewById<TextView> (Resource.Id.txtMemberName);
			memberName.Text = nMembers [position].userName;




			return row;

		}
	}
}
