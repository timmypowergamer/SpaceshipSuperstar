using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour, DunGen.IKeyLock {

	[System.Serializable]
	public class ObjectiveInfo
	{
		public GameObject Prop;
		public string keyName;
	}

	public ObjectiveInfo[] Objectives;

	void Start()
	{
		foreach (ObjectiveInfo o in Objectives) 
		{
			GameManager.instance.RegisterObjective(o);
		}

	}

	#region IKeyLock implementation
	public void OnKeyAssigned (DunGen.Key key, DunGen.KeyManager manager)
	{
		Debug.Log ("ONKeyAssigned" + key.Name);

		foreach (ObjectiveInfo o in Objectives) 
		{
			if (o.keyName == key.Name)
			{
				if (o.Prop != null)
				{
					o.Prop.SetActive(true);
				}
			}
		}
	}
	#endregion
}
