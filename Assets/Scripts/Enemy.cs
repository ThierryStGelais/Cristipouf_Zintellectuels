using UnityEngine;
using System.Collections;

enum EnemyState
{
	DOOR,
	PLAYER,
	WALL
}

public class Enemy : MonoBehaviour {
	Vector2 doorPosition;
	Vector2 evadeWallDest;

    int Health = -1;
    int Strength;

	const float denom = 10;
	EnemyState state = EnemyState.DOOR;

    int indexToPick = -1;

    GameObject[] allDoors;

	GameObject player;

	float playerAttackTimer = 0;
	const float timeBeforePlayerAttack = 1.0f;

	float uqacAttackTimer = 0;
	const float timeBeforeUqacAttack = 1.0f;
	
	DangerGauge life;

	// Use this for initialization
	void Start () {

        allDoors = GameObject.FindGameObjectsWithTag("door");

		indexToPick = Random.Range(0, allDoors.Length);

		GameObject door = allDoors[indexToPick];

		doorPosition = door.transform.position;

		player = GameObject.FindWithTag("player");

		life = GameObject.Find ("dangergauge").GetComponent<DangerGauge>();
	
	}
	
    void Awake()
    {
        Health = (int)30;
        //Health = (int)Mathf.Min(9 + Mathf.Round(Mathf.Pow(1.2f, GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().Wave)), 500.0f);
        //Strength = 9 + (int)Mathf.Round(Mathf.Pow(1.125f, GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().Wave));

		Strength = 0;
    }

	float getTiPitte(Vector2 dest)
	{
		Vector2 distanceVector = dest - (Vector2)transform.position;
		return Mathf.Sqrt (Mathf.Pow(distanceVector.x, 2) + Mathf.Pow(distanceVector.y, 2));
	}

	bool closeEnoughToPlayer()
	{
		float tiPitte = getTiPitte(player.transform.position);

		float playerBounds = player.GetComponent<CircleCollider2D>().radius * 2;

		return tiPitte < playerBounds;
	}

	bool closeEnoughToDoor()
	{
		float tiPitte = getTiPitte (doorPosition);

		return tiPitte < 0.75f;
	}
	
	Vector2 getDirVector(Vector2 dest, Vector2 start)
	{
		Vector2 fullDistance = dest - start;

		float norm = Mathf.Sqrt(Mathf.Pow(fullDistance.x, 2) + Mathf.Pow(fullDistance.y, 2)) * denom;

		return new Vector2(fullDistance.x / norm, fullDistance.y / norm);
	}

	Vector2 move(Vector2 dest)
	{
		Vector2 direction = getDirVector (dest, transform.position);
		
		transform.position = (Vector2)transform.position + direction;

		return -direction;
	}

	void chaseDoor()
	{
		Vector2 reverse = move(doorPosition);

		foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, 2.0f, Vector2.zero))
		{
			if (hit.transform == transform) continue;
			
			if (hit.transform.CompareTag("player"))
			{
				state = EnemyState.PLAYER;
				return;
			}
		}

		if (closeEnoughToDoor())
		{
			transform.position = (Vector2)transform.position + reverse;

			uqacAttackTimer += Time.fixedDeltaTime;

			if (uqacAttackTimer >= timeBeforeUqacAttack)
			{
				life.addDamage(5);

				uqacAttackTimer = 0;
			}
		}
	}

	void chasePlayer()
	{
		Vector2 reverse = move(player.transform.position);

		if (closeEnoughToPlayer ())
		{
			transform.position = (Vector2)transform.position + reverse;

			playerAttackTimer += Time.fixedDeltaTime;
			
			if (playerAttackTimer >= timeBeforePlayerAttack)
			{
				Debug.Log ("TIENS TOÉ!!!");
				playerAttackTimer = 0;
			}
		}
	}

	void avoidWall()
	{
		move(evadeWallDest);

		if (transform.position.x <= evadeWallDest.x + 0.5f && transform.position.x >= evadeWallDest.x - 0.5f)
			state = EnemyState.DOOR;
	}

    void Update()
    {
        if (Health < 0)
        {
            Debug.Log("Il est mort JIM");
            Health = -1;
            gameObject.SetActive(false);
            player.GetComponent<Classes>().ExpPoints += (uint)(4+Mathf.RoundToInt(Mathf.Pow(1.02f,GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().Wave)));
        }
    }

	void FixedUpdate()
	{
		switch (state)
		{
			case EnemyState.DOOR: chaseDoor (); break;
			case EnemyState.WALL: avoidWall (); break;
			case EnemyState.PLAYER: chasePlayer (); break;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//if (other.transform.CompareTag("door"))
			//action en cas de collision

		if (other.transform.CompareTag("wall"))
		{
			state = EnemyState.WALL;

			if (Random.Range (0, 2) == 1)
				evadeWallDest = new Vector2(other.transform.position.x + 2f, transform.position.y);
			else
				evadeWallDest = new Vector2(other.transform.position.x - 2f, transform.position.y);
		}
	}

    public void takeDamage(int damage)
    {
        Health -= damage;
    }
}
