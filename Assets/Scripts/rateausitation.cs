using UnityEngine;
using System.Collections;

public class rateausitation : MonoBehaviour {

    public float firerate;
    public int dmg;
    public int ammo;
    private float nextfire;
    NavAgent enemy;

    // Use this for initialization
    void Start () {
        nextfire = 0;
        dmg = (int)GameObject.Find("joueurFinal(Clone)").GetComponent<Classes>().Technical;
            
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collidee)
    {
        if (Time.time > nextfire && collidee.tag == "enemy" && ammo >= 0)
        {
            enemy = collidee.GetComponentInParent<NavAgent>();
            enemy.takeDamage(dmg);
            nextfire = Time.time + firerate;
            Debug.Log("Murder stuff");
            ammo--;
        }
        if(ammo <= 0)
        {
            Destroy(gameObject);

        }
    }

}
