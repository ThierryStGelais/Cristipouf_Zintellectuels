using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class GameOver_Button : MonoBehaviour {

	AudioSource audio;

	void Start()
	{
		audio = GetComponent<AudioSource>();
	}

	public void ReplayBut()
	{
		Application.LoadLevel("MainMenu");
		audio.Play();
	}
	
	public void QuitterBut()
	{
		Application.Quit();
	}
}
