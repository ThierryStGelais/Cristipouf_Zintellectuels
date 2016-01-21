using UnityEngine;
using System.Collections;

public class class_Heavy : Classes {


	// Use this for initialization
	public void Awake () {
        base.Awake();
        base.Health = 32;
        base.MaxHealth = 32;
        base.Strength = 12;
        base.Speed = 8;
        base.Technical = 9;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//public override void gainEXP(uint exp){base.gainEXP(exp);}

    public override void onLevelUp()
    {
		Debug.Log ("class_Heavy -> onLevelUp");

        base.onLevelUp();
        base.Health += 2;
        base.MaxHealth += 2;
        base.Strength += 2;
        base.Technical += 1;
    }
}
