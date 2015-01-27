using UnityEngine;
using System.Collections;

public class ObjectTrigger : MonoBehaviour {

	public string TriggerObjectID;
	public AudioClip TriggerSound;
	protected GrabableObject triggeredBy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider other)
	{
		GrabableObject grabObject = other.GetComponent<GrabableObject> ();
		if (grabObject != null)
		{
			if(grabObject.ID == TriggerObjectID)
			{
				Trigger (grabObject);
			}
		}
	}

	public virtual void Trigger(GrabableObject triggerObject)
	{
		if (TriggerSound != null && audio != null)
		{
			audio.PlayOneShot (TriggerSound);
		}
		triggeredBy = triggerObject;
	}
}
