using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public List<Objective.ObjectiveInfo> ValidObjectives = new List<Objective.ObjectiveInfo>();

	public float timer = 300;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			Application.Quit();
		}
	}

	public bool RegisterObjective(Objective.ObjectiveInfo objective)
	{
		Debug.Log ("Registering " + objective.keyName);
		if (!ValidObjectives.Contains (objective))
		{
			ValidObjectives.Add(objective);
			PlayerPawn.instance.RecieveMajorNotification("The Station will explode in 5 Minutes!");
			return true;
		}
		return false;
	}

	public void CompleteObjective(string keyName)
	{
		Objective.ObjectiveInfo toComplete = null;
		foreach (Objective.ObjectiveInfo o in ValidObjectives)
		{
			if (o.keyName == keyName)
			{
				toComplete = o;
				break;
			}
		}

		if (toComplete != null)
		{
			ValidObjectives.Remove(toComplete);
			if (ValidObjectives.Count == 0)
			{
				LevelComplete();
			}
		}
	}

	public void LevelComplete()
	{
		PlayerPawn.instance.RecieveMajorNotification ("Mission Complete!");
	}


}
