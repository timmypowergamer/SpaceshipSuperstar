using UnityEngine;
using System.Collections;

public class PlayerPawn : MonoBehaviour {

	public bool mapActive = false;
	public Camera Cam
	{
		get
		{
			if (m_cam == null)
			{
				m_cam = GetComponentInChildren<Camera>();
			}
			return m_cam;
		}
	}
	private Camera m_cam;

	public GameObject GrabSocket;
	public GameObject GrabbedObject = null;

	private Animator m_animController;
	public Animator AnimController
	{
		get
		{
			if (m_animController == null)
			{
				m_animController = GetComponentInChildren<Animator>();
			}
			return m_animController;
		}
	}

	[SerializeField]
	private PlayerNotification notification = null;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RecieveNotification(string message)
	{
		notification.NotificationRecieved (message);
	}
}
