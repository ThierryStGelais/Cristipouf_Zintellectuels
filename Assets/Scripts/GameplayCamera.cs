using UnityEngine;
using System.Collections;

public class GameplayCamera : MonoBehaviour {

	float mapX = 0, mapY = 0;

	float minX, maxX, minY, maxY;

	Camera theCam;

	GameObject thePlayer;

	// Use this for initialization
	void Start () {
	
		theCam = GetComponent<Camera>();

		theCam.orthographicSize = 2.5f;

		thePlayer = GameObject.FindWithTag("player");

	}

	void Update()
	{
		transform.position = new Vector3(thePlayer.transform.position.x,
		                                 10,
		                                 thePlayer.transform.position.z);
	}
	
}
