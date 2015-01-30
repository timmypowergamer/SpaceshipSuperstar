using UnityEngine;
using System.Collections;

public class MonitorSwitch : InteractableObject {

	public SwitchData[] Switches;

	public override void OnUsed(PlayerPawn player)
	{
		bool useDefault = true;
		useCount++;
		if (Switches.Length > 0)
		{
			foreach(SwitchData s in Switches)
			{
				if (s.onUseCount == useCount)
				{
					UseSwitch(s, player);
					useDefault = false;
				}
			}
		}

		if (useDefault)
		{
			UseSwitch(DefaultSwitch, player);
		}
	}
}
