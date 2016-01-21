using UnityEngine;
using System.Collections;

public class DangerGauge : MonoBehaviour {

	float pct = 0;

	const float maxPct = 100;

	float maxVScale;

	// Use this for initialization
	void Start () {

		maxVScale = transform.localScale.y;

		transform.localScale = new Vector3(transform.localScale.x,0,transform.localScale.z);
	
	}
	
	public void addDamage(float amount)
	{
		if (pct >= maxPct) return;

		pct += amount;

		transform.localScale = new Vector3(transform.localScale.x,
								           (pct * maxVScale) / 100,
		                                   transform.localScale.z);

		if (pct >= maxPct)
			Application.LoadLevel (0);
	}

	public void removeDamage(float amount)
	{
		pct -= amount;

		if (pct < 0)
			pct = 0;

		transform.localScale = new Vector3(transform.localScale.x,
		                                   (pct * maxVScale) / 100,
		                                   transform.localScale.z);
	}
}
