using UnityEngine;
using System.Collections;

public class ObjectiveKey : MonoBehaviour, DunGen.IKeySpawnable {

	[System.Serializable]
	public class KeyInfo
	{
		public string keyName;
		public GameObject KeyPrefab;
		public GameObject[] SpawnPoints;
	}

	public KeyInfo[] Keys;

	#region IKeySpawnable implementation

	public void SpawnKey (DunGen.Key key, DunGen.KeyManager manager)
	{
		foreach (KeyInfo k in Keys) 
		{
			if (k.keyName == key.Name)
			{
				GameObject spawnPoint = k.SpawnPoints[Random.Range(0,k.SpawnPoints.Length)];
				Instantiate(k.KeyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
			}
		}
	}

	#endregion



}
