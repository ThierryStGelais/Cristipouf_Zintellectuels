using UnityEngine;
using System.Collections;

public class BookPowerUp : MonoBehaviour {
    public float activationTime=5.0f;
    public float tauntRadius=20.0f;
    private float currentTime;

	// Use this for initialization
	void Start () {
        activationTime = ((float)GameObject.Find("joueurFinal(Clone)").GetComponent<Classes>().Technical)/2;
        currentTime = activationTime;
	}
	
	// Update is called once per frame
	void Update () {
        currentTime -= Time.deltaTime;
        if (currentTime < 0.0f)
        {
            
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        SphereCollider sphere = GetComponent<SphereCollider>();
        Collider[] hitColliders = Physics.OverlapSphere(sphere.center, tauntRadius);
        int i=0;
        while(i<hitColliders.Length){
            if (hitColliders[i].gameObject.GetComponent<NavAgent>())
            {
                hitColliders[i].gameObject.GetComponent<NavAgent>().navAgentTaunt(gameObject.GetComponent<Rigidbody>().position, currentTime);
            }
            i++;
        }
    }

}
