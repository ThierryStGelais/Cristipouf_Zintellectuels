using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

    Vector3[] spawnPoints, spawnPU;
    GameObject[] ennemies;
    public int maxEnnemiesPerWave = 15, maxEnnemiesTotal = 50, Wave = 0;

    float timeBetweenEnnemiesSpawn = 0.5f, timeBetweenPowerUpSpawn = 3.33f;
    bool isSpawning;
    float now;
    public int kills = 0;
    GameObject powerup;
    public GameObject[] PU;
    GameObject PUref;

	// Use this for initialization
	void Awake () {
        isSpawning = false;

        GameObject enemy = Resources.Load("Prefabs/enemyNav") as GameObject;
        powerup = Resources.Load("Prefabs/powerup") as GameObject;
        spawnPoints = new Vector3[transform.GetChild(0).childCount]; 
        ennemies = new GameObject[maxEnnemiesTotal];
        spawnPU = new Vector3[transform.GetChild(1).childCount];
        PU = new GameObject[transform.GetChild(1).childCount];


        for (int i=0; i < transform.GetChild(0).childCount;i++)
        {
            spawnPoints[i] = transform.GetChild(0).GetChild(i).position;
            Debug.Log(transform.GetChild(0).GetChild(i).gameObject.name);
        }
        for (int k = 0; k < transform.GetChild(1).childCount; k++)
        {
            spawnPU[k] = transform.GetChild(1).GetChild(k).position;
            Debug.Log(transform.GetChild(1).GetChild(k).gameObject.name);
        }

        for (int j = 0; j < maxEnnemiesTotal; j++)
        {
            ennemies[j] = (GameObject) Instantiate(enemy, new Vector3(0,0.5f,0),Quaternion.identity);
            ennemies[j].GetComponent<NavAgent>().wave = Wave;
        }
        kills = 0;
        now = Time.time;

	}
	
	// Update is called once per frame
	void Update () 
    {
      //  Debug.Log("Temps: " + (Time.time-now));
	    if (Time.time >= now + 1.75f)
        {
            StartWave();
            now = Mathf.Infinity;
        }
        if (kills == maxEnnemiesPerWave)
        {
            StopWave();
        }

	}

    IEnumerator SpawnEnnemies()
    {
        for (int i = 0; i < maxEnnemiesPerWave; i++)
            {
                int toSpawn = Random.Range(0, transform.GetChild(0).childCount);
                Debug.Log("Spawn @: " + toSpawn);
                ennemies[i].transform.position = new Vector3(spawnPoints[toSpawn].x, 0.5f, spawnPoints[toSpawn].z);
                ennemies[i].SetActive(true);
                yield return new WaitForSeconds(timeBetweenEnnemiesSpawn);
            }
    }

    IEnumerator SpawnPowerUps()
    {
        while(isSpawning)
        {
                int toSpawnPU = Random.Range(0, transform.GetChild(1).childCount);
                Debug.Log("SpawnPU @: " + toSpawnPU);
                if (PU[toSpawnPU] == null)
                {
                    PU[toSpawnPU] = (GameObject)Instantiate(powerup, spawnPU[toSpawnPU], Quaternion.identity);
                    PU[toSpawnPU].GetComponent<powerupselection>().PUindex = toSpawnPU;

                }
                yield return new WaitForSeconds(timeBetweenPowerUpSpawn);
        }
    }

    void StartWave()
    {
        isSpawning = true;
        maxEnnemiesPerWave += 1;
        StartCoroutine(SpawnEnnemies());
        StartCoroutine(SpawnPowerUps());
    }

    void StopWave()
    {
        Debug.Log("radis");
        isSpawning = false;
        StopAllCoroutines();
        Wave++;
        kills = 0;
        now = Time.time;
    }
}
