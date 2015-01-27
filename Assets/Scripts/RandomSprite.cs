using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSprite : MonoBehaviour {

	public Sprite[] SpriteList;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = SpriteList [Random.Range (0, SpriteList.Length)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
