using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public Powerup myPowerup;
    public GameObject[] traps;
    public Transform spawntransform;

    // Use this for initialization
    void Start () {


        traps = Resources.LoadAll<GameObject>("Traps") ;
        Debug.Log(traps[0].ToString());

        //myPowerup = new Mine();

        
        //ActivatePowerup(GetTrapNumber());
        
    }
	
	// Update is called once per frame
	void Update () {
	


	}

    public void ActivatePowerup(int TrapNumber) {

        if (myPowerup != null)
        {
            Instantiate(traps[TrapNumber], spawntransform.position , spawntransform.rotation);
            
        }
        else
            Debug.Log("YOU DON'T HAVE A POWERUP YOU FUCKING RETARD");
    }

    public int GetTrapNumber() {

        return myPowerup.GetNumber();

    }

}


public class Powerup {

    

    public virtual int GetNumber() {
       
        return -1;

    }

}

public class Mur : Powerup {


    public Mur() {


    }

    public override int GetNumber() {

        return 0;

    }

}

public class Mine : Powerup
{

    public Mine()
    {

    }


    public override int GetNumber()
    {


        return 2;

    }


}

public class Rateau : Powerup
{

    public Rateau()
    {

    }


    public override int GetNumber()
    {


        return 3;

    }


}

public class Livre : Powerup
{

    public Livre()
    {

    }


    public override int GetNumber()
    {


        return 1;

    }


}