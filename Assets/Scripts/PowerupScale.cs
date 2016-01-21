using UnityEngine;
using System.Collections;

public class PowerupScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(transform.position.x,
		                                         0.5f,
		                                         transform.position.z);
	
	}
}
