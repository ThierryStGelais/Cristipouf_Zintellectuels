using UnityEngine;
using System.Collections;

public class class_Tech : Classes {



	// Use this for initialization
	public void Awake(){

        base.Awake();
        base.Health = 30;
        base.MaxHealth = 30;
        base.Strength = 8;
        base.Speed = 9;
        base.Technical = 12;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//public override void gainEXP(uint exp){base.gainEXP(exp);}

     public override void onLevelUp()
    {
		Debug.Log ("class_Tech -> onLevelUp");

        base.onLevelUp();
        base.Health += 1;
        base.MaxHealth += 1;
        base.Speed += 1;
        base.Technical += 2;
    }
}
