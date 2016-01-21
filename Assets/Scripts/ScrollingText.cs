using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class ScrollingText : MonoBehaviour {

    private string introText = "Maire de la ville: \nJe lance cet appel pour qu'on se mobilise... \nSi ça continue comme ça, il n'y aura plus d'emplois \npour nos travailleurs. Votre mission est d'empêcher les \nétudiants de s'instruire. \nMobilisons-nous contre les zintellectuels de ce monde.";
	private string displayText;
    private bool routinedone = false;
    public Texture textscreen;
	AudioSource audio;

    void OnGUI()
    {
		GUI.DrawTexture(new Rect(100, 280, 335, 135), textscreen);
        GUI.Label(new Rect(110, 290, 325, 125), displayText);
        
    }

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
        displayText = introText.Substring(0, 19);
        routinedone = false;
        StartCoroutine(MyCoroutine(introText));

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.anyKey && routinedone) {
			Application.LoadLevel ("levelFINAL");
		}
	}

    IEnumerator MyCoroutine (string introText)
    {
        yield return new WaitForSeconds(1.5f);

        for (int i = 19; i <= introText.Length; i++)
        {
            if (!Input.anyKey)
            {
                displayText = introText.Substring(0, i);
				audio.Play();
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                displayText = introText.Substring(0, i);
                yield return new WaitForSeconds(0.01f);

            }

        }

        routinedone = true;
        yield return null;
    }

}
