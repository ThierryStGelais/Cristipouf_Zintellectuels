using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawnMachine : MonoBehaviour {

	GameObject enemy;

	List<LinearBezier> spawnZones = new List<LinearBezier>();

	float minRandomTime;
	float maxRandomTime;

	float timer = 0;
	float currentRandomTime;

	bool notSet = true;

	int numEnemies;

	GameControl gameControl;

	public bool empty(){return numEnemies <= 0;}

	void Start()
	{
		gameControl = GameControl.getInstance ().GetComponent<GameControl>();

		enemy = Resources.Load ("Prefabs/enemy") as GameObject;

		numEnemies =  5 + (5 * gameControl.Wave);

		minRandomTime = 1.0f / ((float)gameControl.Wave - 0.5f);
		maxRandomTime = 2.0f / ((float)gameControl.Wave - 0.5f);
	}

	void Update()
	{
		if (notSet) return;
		if (empty ()) return;

		timer += Time.deltaTime;

		if (timer >= currentRandomTime)
		{
			int randomZoneId = Random.Range (0, spawnZones.Count);

			LinearBezier randomZone = spawnZones[randomZoneId];

			Vector2 spawnPoint = randomZone.getPoint(Random.Range (0.0f, 1.0f));

			Instantiate (enemy, spawnPoint, new Quaternion());

			--numEnemies;

			currentRandomTime = Random.Range(minRandomTime, maxRandomTime);
			timer = 0;
		}
	}

	public void setMachine(LinearBezier spawnZone)
	{
		List<LinearBezier> intoAList = new List<LinearBezier>();
		intoAList.Add (spawnZone);

		setMachine(intoAList);
	}

	public void setMachine(List<LinearBezier> spawnZones)
	{
		this.spawnZones = spawnZones;
		
		currentRandomTime = Random.Range(minRandomTime, maxRandomTime);

		notSet = false;
	}
}
