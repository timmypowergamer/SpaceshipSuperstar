using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour {
	
	public AudioClip OnEnterSound;
	public AudioClip OnExitSound;
	public string OnEnterAnim;
	public string OnExitAnim;
	public string OnEnterMethod;
	public string OnExitMethod;
	public GameObject MethodHandler;

	void Start()
	{
		if (MethodHandler == null)
		{
			MethodHandler = gameObject;
		}
	}

	
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			Trigger();
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			UnTrigger();
		}
	}
	
	public virtual void Trigger()
	{
		if (OnEnterSound != null && audio != null)
		{
			audio.PlayOneShot (OnEnterSound);
		}
		if (animation != null)
		{
			if (!string.IsNullOrEmpty(OnEnterAnim))
			{
				animation.Play(OnEnterAnim);
			}
			else if (!string.IsNullOrEmpty(OnExitAnim))
			{
				animation.Rewind(OnExitAnim);
			}
		}
		if (!string.IsNullOrEmpty(OnEnterMethod))
		{
			MethodHandler.SendMessage(OnEnterMethod);
		}
	}

	public virtual void UnTrigger()
	{
		if (OnExitSound != null && audio != null)
		{
			audio.PlayOneShot (OnExitSound);
		}
		if (animation != null)
		{
			if (!string.IsNullOrEmpty(OnExitAnim))
			{
				animation.Play(OnExitAnim);
			}
			else if (!string.IsNullOrEmpty(OnEnterAnim))
			{
				animation.Rewind(OnEnterAnim);
			}
		}
		if (!string.IsNullOrEmpty(OnExitMethod))
		{
			MethodHandler.SendMessage(OnExitMethod);
		}
	}
}
