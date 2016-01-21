using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITextUpdate : MonoBehaviour {

    private int wave;

	// Use this for initialization
	void Start () {
        Debug.Log("C'est creer");
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log("Wave numero : " + wave);
        wave = GameObject.FindGameObjectWithTag("WaveControl").GetComponent<WaveSpawner>().Wave;
        GameObject.FindGameObjectWithTag("NumeroVagueText").GetComponent<Text>().text = wave.ToString();
	}
}
