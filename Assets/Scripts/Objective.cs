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

	#region IKeyLock implementation
	public void OnKeyAssigned (DunGen.Key key, DunGen.KeyManager manager)
	{
		Debug.Log ("ONKeyAssigned" + key.Name);

		foreach (ObjectiveInfo o in Objectives) 
		{
			if (o.keyName == key.Name)
			{
				o.Prop.SetActive(true);
				GameManager.instance.RegisterObjective(o);
				InteractableObject io = o.Prop.GetComponentInChildren<InteractableObject>();
				if (io != null)
				{
					io.keyName = o.keyName;
				}
			}
		}
	}
	#endregion
}
