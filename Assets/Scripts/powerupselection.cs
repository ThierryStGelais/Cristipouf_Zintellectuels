using UnityEngine;
using System.Collections;

public class powerupselection : MonoBehaviour {

    public enum Trap {Mur,Livre,Mine,Rateau};
    public Trap myTrap;
    public int PUindex = -1;

	// Use this for initialization
	void Start () {

        myTrap = (Trap)Random.Range(0,3);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collidee)
    {

        if(collidee.tag == "player")
        {

            if (!(collidee.gameObject.GetComponent<PlayerControl>().HasPowerUp()))
            {
                collidee.GetComponent<PlayerControl>().addItem((int)myTrap);
                GameObject.FindGameObjectWithTag("WaveControl").GetComponent<WaveSpawner>().PU[PUindex] = null;
                Debug.Log(GameObject.FindGameObjectWithTag("WaveControl").GetComponent<WaveSpawner>().PU[PUindex]);
                Destroy(this.gameObject);
            }

        }
            

    }

    public GameObject RemoveRef()
    {
        return null;
    }

}
