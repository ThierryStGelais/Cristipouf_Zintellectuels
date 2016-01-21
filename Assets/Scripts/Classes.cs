using UnityEngine;
using System.Collections;

public class Classes : MonoBehaviour {

    public uint Health, Strength, Speed, Technical, Level, ExpPoints, MaxHealth;
    public const int Intelligence = -1;

	protected AudioSource audioS;

	protected AudioClip levelUp, hit;

	// Use this for initialization
    public virtual void Awake()
    {
        Level = 1;
        ExpPoints = 0;

		audioS = GetComponent<AudioSource>();

		levelUp = Resources.Load<AudioClip> ("Audio/LevelUp");
		hit = Resources.Load<AudioClip> ("Audio/hit");
    }

	public virtual uint getStrength(){return Strength;}

	public virtual void gainEXP(uint exp)
	{
		ExpPoints += exp;

		if (ExpPoints >= 100)
			onLevelUp ();
	}

    // Called when character reaches 100 Experience Points
    public virtual void onLevelUp()
    {
		Debug.Log ("Classes -> onLevelUp");

		audioS.PlayOneShot(levelUp);

        Health+=3;
        MaxHealth += 3;
        Strength++;
        Speed++;
        Technical++;
        Level++;
        ExpPoints %= 100;
    }

	public virtual void getDamaged(uint damage)
	{
		if (damage >= Health)
		{
			Application.LoadLevel ("gameOver");
			return;
		}

		audioS.PlayOneShot(hit);

		Health -= damage;
        GameObject.FindGameObjectWithTag("player").GetComponent<PlayerControl>().currentHealth = Health;
		Debug.LogWarning ("Health : " + Health);
	}
}
