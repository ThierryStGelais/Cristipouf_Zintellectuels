using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class MainMenuButtons : MonoBehaviour {
	
	AudioSource audio;

	void Start()
	{
		audio = GetComponent<AudioSource>();
	}

	public void btnStartGame()
	{
		Application.LoadLevel("classSelect");
		audio.Play();
	}
	
	public void btnExitGame()
	{
		Application.Quit();
	}
}
