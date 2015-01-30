using UnityEngine;
using System.Collections;

public class GrabableObject : MonoBehaviour {

	public string ID;
	public PlayerPawn player;
	public string keyName;

	public int originalLayer;

	// Use this for initialization
	void Start () {
		originalLayer = gameObject.layer;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
