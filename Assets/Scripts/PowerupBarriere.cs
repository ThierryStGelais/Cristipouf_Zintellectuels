using UnityEngine;
using System.Collections;

public class PowerupBarriere : MonoBehaviour {
    public float m_maxTime;
    private float p_currentTime;


	// Use this for initialization
	void Start () {
        m_maxTime = ((float)GameObject.Find("joueurFinal(Clone)").GetComponent<Classes>().Technical) / 2;
        p_currentTime = m_maxTime;
	}

	
	// Update is called once per frame
	void Update () {
        p_currentTime -= Time.deltaTime;
        if (p_currentTime < 0.0f)
        {
            gameObject.SetActive(false);
        }
	}
}
