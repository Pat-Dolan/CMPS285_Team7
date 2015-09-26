
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
	[Activity (Label = "DBAccessActivity")]			
	public class DBAccessActivity : Activity
	{
		int id;
		EditText Id;
		EditText Username;
		EditText Password;
		EditText ProfileName;
		EditText Access;
		EditText FName;
		EditText LName;
		EditText Email;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			id = 1;
			SetContentView (Resource.Layout.DBAccessLayout);

			//Create your application here
			Button btnNext = FindViewById<Button>(Resource.Id.btnDBNext);
			btnNext.Click += BtnNext_Click;

			Id = FindViewById<EditText> (Resource.Id.txtDBId);
			Id.Text = id.ToString ();
			Username = FindViewById<EditText> (Resource.Id.txtDBUsername);
			Password = FindViewById<EditText> (Resource.Id.txtDBPassword);
			ProfileName = FindViewById<EditText> (Resource.Id.txtDBProfileName);
			Access = FindViewById<EditText> (Resource.Id.txtDBAccess);
			FName = FindViewById<EditText> (Resource.Id.txtDBFirstName);
			LName = FindViewById<EditText> (Resource.Id.txtDBLastName);
			Email = FindViewById<EditText> (Resource.Id.txtDBEmail);

			Button getAll = FindViewById<Button> (Resource.Id.btnGetAll);
			getAll.Click += GetAll_Click;

		}

		void GetAll_Click (object sender, EventArgs e)
		{
			DBRepository dbr = new DBRepository ();
			string result = dbr.GetAccount ();
			Toast.MakeText (this, result, ToastLength.Short).Show ();
		}

		void BtnNext_Click (object sender, EventArgs e)
		{
			//Add additional fields as necessary
			DBRepository dbr = new DBRepository ();
			var user = dbr.GetUserById (id);

			//setting texts to information
			Id.Text = (user.ID).ToString();
			Username.Text = user.UserName;
			Password.Text = user.Pass;
			ProfileName.Text = user.ProfileName;
			Access.Text = (user.Access).ToString ();
			FName.Text = user.FirstName;
			LName.Text = user.LastName;
			Email.Text = user.Email;
			//Moving to next Table;
			id++;


		}
	}
}

