using UnityEngine;
using System.Collections;

public class EnemyKillerScript : MonoBehaviour {

	EnemySpawnMachine spawnMachine;
	Classes playerClass;

	void Start()
	{
		spawnMachine = GameObject.Find ("EnemySpawnMachine").GetComponent<EnemySpawnMachine>();

		foreach(GameObject thing in GameObject.FindGameObjectsWithTag("Player"))
		{
			if (thing.name != "Personnage")
			{
				/*if (thing.name.Contains("Heavy"))
					playerClass = thing.GetComponent<class_Heavy>();
				else if (thing.name.Contains("Tech"))
					playerClass = thing.GetComponent<class_Tech>();
				else if (thing.name.Contains ("Light"))
					playerClass = thing.GetComponent <class_Light>();*/

				playerClass = thing.GetComponent<Classes>();

				
				break;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

			if (enemies.Length > 0)
			{
				int toKill = Random.Range (0, enemies.Length);

				Destroy (enemies[toKill]);

				playerClass.gainEXP(25);

				if (spawnMachine.empty())
				{
					Debug.Log ("Good job");
					Application.LoadLevel (0);
				}
			}
		}
	}
}
