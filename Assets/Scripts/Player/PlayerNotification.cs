using UnityEngine;
using System.Collections;

public class PlayerNotification : MonoBehaviour {

	public UILabel messageLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NotificationRecieved(string message)
	{
		messageLabel.text = message;
		CancelInvoke ();
		Invoke ("Dismiss", 4f);
	}

	public void Dismiss()
	{
		messageLabel.text = "";
	}
}
