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
		int id = 0;
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

			//Setting layout
			SetContentView (Resource.Layout.DBAccessLayout);

			//Assigning text fields
			Id = FindViewById<EditText> (Resource.Id.txtDBId);
			Username = FindViewById<EditText> (Resource.Id.txtDBUsername);
			Password = FindViewById<EditText> (Resource.Id.txtDBPassword);
			ProfileName = FindViewById<EditText> (Resource.Id.txtDBProfileName);
			Access = FindViewById<EditText> (Resource.Id.txtDBAccess);
			FName = FindViewById<EditText> (Resource.Id.txtDBFirstName);
			LName = FindViewById<EditText> (Resource.Id.txtDBLastName);
			Email = FindViewById<EditText> (Resource.Id.txtDBEmail);

			//Adding Components
			Button btnNext = FindViewById<Button>(Resource.Id.btnDBNext);
			btnNext.Click += BtnNext_Click;

			Button getAll = FindViewById<Button> (Resource.Id.btnGetAll);
			getAll.Click += GetAll_Click;

			Button btnPrevious = FindViewById<Button> (Resource.Id.btnDBPrevious);
			btnPrevious.Click += BtnPrevious_Click;

		}

		//This function shows the previous user
		void BtnPrevious_Click (object sender, EventArgs e)
		{
			if (id > 1) 
			{
				id--;
				DisplayTable (id);
			}
			Id.Text = id.ToString ();
		}

		//This function displays all users as a message
		void GetAll_Click (object sender, EventArgs e)
		{
			DBRepository dbr = new DBRepository ();
			string result = dbr.GetAccount ();
			Toast.MakeText (this, result, ToastLength.Short).Show ();
		}

		//This function displays the next user
		void BtnNext_Click (object sender, EventArgs e)
		{
			//Moving to next Table;
			id++;
			DisplayTable (id);
			Id.Text = id.ToString ();
		}


		//This function updates the text fields in the layout to relect user information by id number
		void DisplayTable(int id)
		{
			DBRepository dbr = new DBRepository ();
			var user = dbr.GetUserById (id);

			//Setting text fields to information
			Id.Text = (user.ID).ToString();
			Username.Text = user.UserName;
			Password.Text = user.Pass;
			ProfileName.Text = user.ProfileName;
			Access.Text = (user.Access).ToString ();
			FName.Text = user.FirstName;
			LName.Text = user.LastName;
			Email.Text = user.Email;
		}
	}
}

