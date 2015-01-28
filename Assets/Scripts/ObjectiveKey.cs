using UnityEngine;
using System.Collections;

public class ObjectiveKey : MonoBehaviour, DunGen.IKeySpawnable {

	public Objective.ObjectiveInfo[] Objectives;

	#region IKeySpawnable implementation

	public void SpawnKey (DunGen.Key key, DunGen.KeyManager manager)
	{
		foreach (Objective.ObjectiveInfo o in Objectives) 
		{
			if (o.keyName == key.Name)
			{
				o.Prop.SetActive(true);
				GrabableObject go = o.Prop.GetComponentInChildren<GrabableObject>();
				if (go != null)
				{
					go.keyName = o.keyName;
				}
			}
		}
	}

	#endregion



}
