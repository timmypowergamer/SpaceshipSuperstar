using UnityEngine;
using System.Collections;

public class TimerDisplay : MonoBehaviour {

	public UILabel label;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		System.TimeSpan timer = System.TimeSpan.FromSeconds((double)GameManager.instance.timer);

		if (timer.Hours > 0)
		{
			label.text = timer.Hours.ToString() + ":" + timer.Minutes.ToString("0#") + ":" + timer.Seconds.ToString("0#");
		}
		else
		{
			label.text = timer.Minutes.ToString("0#") + ":" + timer.Seconds.ToString("0#");
		}

		if (timer.Minutes < 2)
		{
			label.color = Color.red;
		}
	}
}
