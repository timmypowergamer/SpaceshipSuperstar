using UnityEngine;
using System.Collections;

public class ToiletTrigger : ObjectTrigger {

	public GameObject Plunger;
	public GameObject FlushTrigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Trigger (GrabableObject triggerObject)
	{
		base.Trigger (triggerObject);
		animation.Play ("Plunge");
		triggeredBy.rigidbody.isKinematic = true;
		triggeredBy.transform.position = Plunger.transform.position;
		triggeredBy.transform.rotation = Plunger.transform.rotation;
		triggeredBy.gameObject.SetActive (false);
		collider.enabled = false;
	}

	public void SendPlungeMessage()
	{
		triggeredBy.player.RecieveNotification ("Blockage Removed");
		triggeredBy.gameObject.SetActive (true);
		triggeredBy.rigidbody.isKinematic = false;
		FlushTrigger.SetActive (true);
		Plunger.SetActive (false);
	}

	public void PlayPlungeSound()
	{
		audio.Play ();
	}
}
