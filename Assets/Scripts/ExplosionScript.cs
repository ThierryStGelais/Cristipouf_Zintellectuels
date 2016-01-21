using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	float time = 0;
	public float timeToLive;
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (time >= timeToLive)
			DestroyObject (gameObject);
	}
}
