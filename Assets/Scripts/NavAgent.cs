using UnityEngine;
using System.Collections;

public class NavAgent : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;
    GameObject player;
    int Health, Strength;
    private float currentTauntTime=0.0f;
    private Vector3 tauntLocation;
    public int wave;
    int p_maxHealth;

    public Texture2D emptyTex;
    public Texture2D fullTex;

    public GUIStyle progress_empty;
    public GUIStyle progress_full;

    public Vector2 sizeOfBar = new Vector2(50,10);
    public Vector3 translationOfBar = new Vector3(-25, 30, 0);

	int enemyNum = 0;
    

	public void setEnemyNum(int num)
	{
		enemyNum = num;
	}
    

    
    // Use this for initialization
	void OnEnable () {

        Health = (int)Mathf.Min(9 + Mathf.Round(Mathf.Pow(1.2f, wave)), 500.0f);
        Strength = (int)Mathf.Min(9 + (int)Mathf.Round(Mathf.Pow(1.125f,wave)), 400);
        p_maxHealth = Health;
	}

    void OnDisable()
    {
        //GameControl.getInstance().GetComponent<GameControl>().script.onLevelUp();
        GameObject.FindGameObjectWithTag("WaveControl").GetComponent<WaveSpawner>().kills++;
        Debug.Log(GameObject.FindGameObjectWithTag("WaveControl").GetComponent<WaveSpawner>().kills);
    }
	
	// Update is called once per frame
	void Update () {
        currentTauntTime -= Time.deltaTime;
        if (currentTauntTime<0.1f)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(tauntLocation);
        }
        float dist = agent.remainingDistance;
        if (!agent.pathPending && currentTauntTime < 0.0f)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    Debug.Log("potato life");
                    damageUQAC();
                }
            }
        }
        if (Health < 1)
        {
            //Debug.Log("Il est mort JIM");
            Health = -1;
            player.GetComponent<Classes>().gainEXP((uint)(25 +2*( Mathf.RoundToInt(Mathf.Pow(1.07f, GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().Wave)))));
            //Debug.Log("EXP: "+(4 + Mathf.RoundToInt(Mathf.Pow(1.02f, GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().Wave))));

			string path = "Prefabs/explosions/explosion"  + (enemyNum+1);

			GameObject explosion = Instantiate (Resources.Load (path), transform.position, new Quaternion()) as GameObject;

			explosion.transform.eulerAngles = new Vector3(90, 0, 0);
			explosion.transform.position = new Vector3(explosion.transform.position.x,
			                                           0.5f,
			                                           explosion.transform.position.z);

            gameObject.SetActive(false);
        }
        
	}

    void Awake()
    {
        p_maxHealth = Health;
        agent = GetComponent<NavMeshAgent>();
        //Debug.Log("patate");
        target = GameObject.FindGameObjectWithTag("Target").transform;

    }

    void Start()
    {
        player = GameObject.FindWithTag("player");
    
    }

    public void takeDamage(int damage)
    {
        
        Health -= damage;
        Debug.Log("Health remaining:" + Health);
    }


	void damageUQAC()
	{
		GameControl.instance.GetComponent<GameControl>().damage_UQAC(2* Strength);
		gameObject.SetActive(false);
	}


    public void navAgentTaunt(Vector3 location, float time)
    {
        Debug.Log("navAngentTaunt est lancé!");
        currentTauntTime = time;
        tauntLocation = location;
    }

    void OnGUI()
    {
        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position) + translationOfBar;

        GUI.BeginGroup(new Rect(targetPos.x, Screen.height - targetPos.y, sizeOfBar.x, sizeOfBar.y), emptyTex, progress_empty);
        GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, sizeOfBar.x, sizeOfBar.y), fullTex, progress_full);

        GUI.BeginGroup(new Rect(0, 0, sizeOfBar.x * (float)Health / p_maxHealth, sizeOfBar.y));
        GUI.Box(new Rect(0, 0, sizeOfBar.x, sizeOfBar.y), fullTex, progress_full);

        GUI.EndGroup();
        GUI.EndGroup();


    }

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("player"))
			other.GetComponent<Classes>().getDamaged (5);
	}

}
