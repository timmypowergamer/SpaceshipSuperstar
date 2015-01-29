﻿using UnityEngine;
using System.Collections;

public class PlayerUse : MonoBehaviour {

	public float ThrowForce = 5f;

	PlayerPawn m_player;
	public PlayerPawn Player
	{
		get
		{
			if (m_player == null)
			{
				m_player = NGUITools.FindInParents<PlayerPawn>(transform);
			}
			return m_player;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Use"))
		{
			if (Player.GrabbedObject == null)
			{
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(Player.Cam.transform.position, Player.Cam.transform.forward, out hit, 10f))
				{
					GrabableObject go = hit.transform.GetComponent<GrabableObject>();
					if (go != null)
					{
						hit.rigidbody.isKinematic = true;
						//hit.rigidbody.useGravity = false;//
						hit.transform.parent = Player.GrabSocket.transform;
						hit.transform.localPosition = Vector3.zero;
						hit.transform.localRotation = Quaternion.identity;
						Player.GrabbedObject = hit.transform.gameObject;
						go.player = Player;
						Player.AnimController.SetBool("PlayGrab",true);
						Physics.IgnoreCollision(Player.collider, hit.collider, true);
					}
					else
					{
						Player.AnimController.SetTrigger("PlaySlap");
					}
					//if (hit.transform.GetComponent<InteractableObject>() != null)
					//{
					//	hit.transform.GetComponent<InteractableObject>().OnUsed(Player);
					//}
				}
				else
				{
					Player.AnimController.SetTrigger("PlaySlap");
				}
			}
			else
			{
				Player.GrabbedObject.rigidbody.isKinematic = false;
				//Player.GrabbedObject.rigidbody.useGravity = true;
				Player.GrabbedObject.transform.parent = null;//
				Player.GrabbedObject.rigidbody.AddForce(Player.Cam.transform.forward * ThrowForce);
				Physics.IgnoreCollision(Player.collider, Player.GrabbedObject.rigidbody.collider, false);
				Player.GrabbedObject = null;
				Player.AnimController.SetBool("PlayGrab",false);
				Player.AnimController.SetTrigger("Throw");

			}
		}
	}

	public void SlapComplete()
	{
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast (Player.Cam.transform.position, Player.Cam.transform.forward, out hit, 10f)) 
		{
			if (hit.transform.GetComponent<InteractableObject> () != null)
			{
				hit.transform.GetComponent<InteractableObject> ().OnUsed (Player);
			}
		}
	}
}