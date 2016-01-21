using UnityEngine;
using System;
using System.Collections;


public class Sword : MonoBehaviour {


	GameObject holder;

	BoxCollider itsCollider;

	bool swingRight = false;

	bool ready = false;

	const float swingSpeed = 750;

	float currentSwingAngle = 0;
	const float swingAngle = 120;

	// Use this for initialization
	void Start () {

		itsCollider = GetComponent<BoxCollider>();

	}
	
	public void setHolder(GameObject holder)
	{
		this.holder = holder;

		ready = true;

		transform.RotateAround(holder.transform.position, Vector3.up, (swingAngle / 2) + 2*transform.eulerAngles.y);
	}
	

	// transform.eulerAngles.y
	void Update () {
		if (!ready) return;

		if (itsCollider.enabled)
		{
            if (!swingRight)
            {
                currentSwingAngle += Time.deltaTime * swingSpeed;
                transform.RotateAround(holder.transform.position, Vector3.up, Time.deltaTime * -swingSpeed);

                if (currentSwingAngle >= swingAngle)
                {
                    float correction = currentSwingAngle - swingAngle;
                    transform.RotateAround(holder.transform.position, Vector3.up, correction);

                   // itsCollider.enabled = false;
                    swingRight = true;
                    currentSwingAngle = 0;
                }
            }
			if (swingRight)
			{
				currentSwingAngle += Time.deltaTime * swingSpeed;
				transform.RotateAround(holder.transform.position, Vector3.up, Time.deltaTime * swingSpeed);

				if (currentSwingAngle >= swingAngle)
				{
					float correction = swingAngle - currentSwingAngle;
					transform.RotateAround(holder.transform.position, Vector3.up, correction);

					itsCollider.enabled = false;
					swingRight = false;
					currentSwingAngle = 0;
				}
			}
			
		}
	
	}

	public void Attack()
	{
		itsCollider.enabled = true;
	}

	public void setSprite(Sprite newSprite)
	{
		GetComponent<SpriteRenderer>().sprite = newSprite;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("enemy"))
		{
			Vector2 difference = new Vector2(other.transform.position.x - holder.transform.position.x,
			                                 other.transform.position.z - holder.transform.position.z);

			float diffNorm = Mathf.Sqrt(Mathf.Pow(difference.x, 2) + Mathf.Pow (difference.y, 2));
			Vector3 diffUnit = new Vector3(difference.x / diffNorm, 0, difference.y / diffNorm);

			other.attachedRigidbody.AddForce(diffUnit * 333);

			other.GetComponent<NavAgent>().takeDamage (Convert.ToInt32(GameObject.FindWithTag ("player").GetComponent<Classes>().getStrength()));
		}
	}
}
