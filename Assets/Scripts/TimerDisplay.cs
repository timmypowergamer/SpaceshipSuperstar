using UnityEngine;
using System.Collections;

public class TimerDisplay : MonoBehaviour {

	public UILabel label;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		label.text = Mathf.RoundToInt (GameManager.instance.timer).ToString ();
	}
}
