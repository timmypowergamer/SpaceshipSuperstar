using UnityEngine;
using System.Collections;

public class IOCterrain : IOCcomp {

	private IOCcam iocCam;
	private bool hidden;
	private int counter;
	private int frameInterval;
	private Terrain terrain;

	void Awake () {
		iocCam =  Camera.main.GetComponent<IOCcam>();
		if(iocCam == null)
		{
			this.enabled = false;
		}
		else
		{
			terrain = GetComponent<Terrain>();
		}
	}
	
	void Start () {
		terrain.enabled = false;
	}

	void Update () {
		frameInterval = Time.frameCount % 4;
		if(frameInterval == 0){
			if(!hidden && Time.frameCount - counter > iocCam.hideDelay)
			{
				Hide();
			}
		}
	}

	public void Hide(){
		terrain.enabled = false;
		hidden = true;
	}

	public override void UnHide(RaycastHit hit) {
		counter = Time.frameCount;
		terrain.enabled = true;
		hidden = false;
	}
}
