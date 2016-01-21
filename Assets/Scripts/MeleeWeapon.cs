using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour {
    public float m_ForceAtImpact=250;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "enemy")
        {

            Debug.Log("Ennemi touché!");
            coll.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(coll.transform.position.x - this.GetComponentInParent<Transform>().position.x, coll.transform.position.x - this.GetComponentInParent<Transform>().position.x) * m_ForceAtImpact, coll.transform.position);
            coll.gameObject.GetComponent<Enemy>().takeDamage(GetComponentInParent<CharacterController>().m_Damage);
        }
    }
    
}
