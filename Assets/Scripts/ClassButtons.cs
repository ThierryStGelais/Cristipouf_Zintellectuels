using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class ClassButtons : MonoBehaviour {

	GameControl gameControl;
	AudioSource audio;

	void Start()
	{
		gameControl = GameControl.getInstance ().GetComponent<GameControl>();
		audio = GetComponent<AudioSource>();
	}

	public void btnClassSelect(int type)
	{
		gameControl.playerType = type;
		Debug.Log("onClick " + type);
        Application.LoadLevel("Intro");
		audio.Play();
	}
}
