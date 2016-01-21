using UnityEngine;
using System.Collections;

public class explosiveness : MonoBehaviour {

    public int explosionradius = 5;
    public int dmg;
    NavAgent enemy;
    public Collider[] hitColliders;

    // Use this for initialization
    void Start () {

        dmg = (int)GameObject.Find("joueurFinal(Clone)").GetComponent<Classes>().Technical;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collidee)
    {
        if (collidee.tag.Equals("enemy"))
        {
            bool hasEnemies = false;
            
            hitColliders = Physics.OverlapSphere(gameObject.transform.position, explosionradius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == "enemy")
                {
                    enemy = hitColliders[i].GetComponentInParent<NavAgent>();
                    enemy.takeDamage(dmg);
                    Debug.Log("DESTROY THAT SHIT");
                    hasEnemies = true;
                }
                i++;
            }
            if (hasEnemies)
            {
                Destroy(gameObject);
            }
        }

    }
}
