package md5f424497de6707ca4dc3b401f9326f17a;


public class DBAccessActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("AutomaTech.DBAccessActivity, AutomaTech, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DBAccessActivity.class, __md_methods);
	}


	public DBAccessActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DBAccessActivity.class)
			mono.android.TypeManager.Activate ("AutomaTech.DBAccessActivity, AutomaTech, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
