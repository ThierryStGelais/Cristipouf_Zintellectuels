using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupSpawnMachine : MonoBehaviour {

	GameObject powerup;

	List<Rect> spawnZones;

	float minRandomTime;
	float maxRandomTime;
	
	float timer = 0;
	float currentRandomTime;
	
	bool notSet = true;

	// Use this for initialization
	void Start () {
		powerup = Resources.Load ("Prefabs/powerup") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (notSet) return;
		
		timer += Time.deltaTime;
		
		if (timer >= currentRandomTime)
		{
			int randomZoneId = Random.Range (0, spawnZones.Count);
			
			Rect randomZone = spawnZones[randomZoneId];
			
			Vector2 spawnPoint = new Vector2(Random.Range(randomZone.xMin, randomZone.xMax),
			                                 Random.Range(randomZone.yMin, randomZone.yMax));
			
			Instantiate (powerup, spawnPoint, new Quaternion());
			
			currentRandomTime = Random.Range(minRandomTime, maxRandomTime);
			timer = 0;
		}
	}

	public void setMachine(Rect spawnZone, float minRandomTime, float maxRandomTime)
	{
		List<Rect> intoAList = new List<Rect>();
		intoAList.Add (spawnZone);
		
		setMachine(intoAList, minRandomTime, maxRandomTime);
	}
	
	public void setMachine(List<Rect> spawnZones, float minRandomTime, float maxRandomTime)
	{
		this.spawnZones = spawnZones;
		this.minRandomTime = minRandomTime;
		this.maxRandomTime = maxRandomTime;
		
		currentRandomTime = Random.Range(minRandomTime, maxRandomTime);
		
		notSet = false;
	}
}
