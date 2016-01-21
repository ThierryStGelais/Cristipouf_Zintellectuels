using UnityEngine;
using System.Collections;

public class class_Light : Classes {

	// Use this for initialization
	public void Awake () {
        base.Awake();
        base.Health = 27;
        base.MaxHealth = 27;
        base.Strength = 8;
        base.Speed = 12;
        base.Technical = 9;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//public override void gainEXP(uint exp){base.gainEXP(exp);}
    
	public override void onLevelUp()
    {
		Debug.Log ("class_Light -> onLevelUp");

        base.onLevelUp();
        base.Health += 1;
        base.MaxHealth+=1;
        base.Speed += 2;
        base.Technical += 1;
    }
}
