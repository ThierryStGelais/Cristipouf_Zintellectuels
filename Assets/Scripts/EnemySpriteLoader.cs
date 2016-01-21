using UnityEngine;
using System.Collections;

public class EnemySpriteLoader : MonoBehaviour {

	public Sprite[] sprites;

	// Use this for initialization
	void Start ()
	{
		int randomIndex = Random.Range (0, sprites.Length);

		GetComponent<SpriteRenderer>().sprite = sprites[randomIndex];

		transform.parent.gameObject.GetComponent<NavAgent>().setEnemyNum(randomIndex);
	}
}
