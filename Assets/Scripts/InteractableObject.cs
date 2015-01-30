using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour {


	[System.Serializable]
	public class SwitchData
	{
		public AudioClip OnUseSound;
		public string OnUseMessage = "Space Computer Activated!";
		public string keyName;
		public string clipName;
		public int onUseCount = 0;
	}

	public SwitchData DefaultSwitch = new SwitchData();
	
	protected int useCount = 0;

	public virtual void OnUsed(PlayerPawn player)
	{
		useCount++;
		UseSwitch(DefaultSwitch, player);
	}

	public virtual void UseSwitch(SwitchData sw, PlayerPawn player)
	{
		if (sw != null)
		{
			player.RecieveNotification (sw.OnUseMessage);
			NGUITools.PlaySound (sw.OnUseSound);
			if (!string.IsNullOrEmpty (sw.keyName))
			{
				GameManager.instance.CompleteObjective(sw.keyName);
			}
			
			if (animation != null && !string.IsNullOrEmpty(sw.clipName))
			{
				animation.Play(sw.clipName);
			}
		}
	}
}
