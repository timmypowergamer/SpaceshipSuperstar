using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour {

	public AudioClip OnUseSound;
	public string OnUseMessage = "Space Computer Activated!";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void OnUsed(PlayerPawn player)
	{
		player.RecieveNotification (OnUseMessage);
		NGUITools.PlaySound (OnUseSound);
	}
}
