using UnityEngine;
using System.Collections;

public class InitDemo_2 : MonoBehaviour {
	
	public Transform Prefab;
	public int gridSize;
	public float margin;

	void Awake () {
		for(int i=0; i<gridSize; i++)
		{
			for(int k=0; k<gridSize; k++)
			{
				Instantiate(Prefab, new Vector3(transform.position.x + i * margin, 0f, transform.position.z + k * margin), Quaternion.identity);
			}
		}
	}
}
