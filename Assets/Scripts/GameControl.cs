using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour
{
	public Sprite sprite_Base; // Drag your first sprite here
	public Sprite sprite_Niveau1; // Drag your sprite here
	public Sprite sprite_Niveau2; // Drag your sprite here
	public Sprite sprite_Niveau3; // Drag your sprite here
	public Sprite sprite_Niveau4; // Drag your sprite here
	public Sprite sprite_Niveau5; // Drag your sprite here
	public Sprite sprite_Niveau6; // Drag your sprite here
	
    public static GameObject instance;
    public int playerType;
    public int Wave;
    public float HealthUQAC = 800;
	public float MaxHealthUQAC = 800;
    

    public Classes script;

	EnemySpawnMachine e_spawn;
	PowerupSpawnMachine p_spawn;

	private SpriteRenderer spriteRenderer;

    //Variable pour fadeout
    public Texture2D FadeoutTexture;
    public float FadeoutSpeed = 0.1f;

    private int depth = -1000;
    private float alpha = 0.0f;
    private int fadedirection = -1;

    void Awake()
    {
        if (instance == null) instance = this.gameObject;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this);
		spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
		if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
			spriteRenderer.sprite = sprite_Base;
	}

	public static GameObject getInstance()
	{
		return instance;
	}

    void OnLevelWasLoaded(int level)
    {
        if (this.gameObject == instance)
        {
            if (level == 2)
            {
                spriteRenderer.enabled = true;
                GameObject player = Resources.Load("Player/joueurFinal") as GameObject;
                
                Debug.Log("onLevelWasLoaded " + playerType);

                player = Instantiate(player);

                switch (playerType)
                {
                    case 0:
                        script = player.AddComponent<class_Heavy>(); Debug.Log("case 0");
                        break;
                    case 1:
                        script = player.AddComponent<class_Light>(); Debug.Log("case 1");
                        break;
                    case 2:
                        script = player.AddComponent<class_Tech>(); Debug.Log("case 2");
                        break;
                }

                //player.transform.position = new Vector3(0, 0, 0);

              /*  switch (level)
                {
                    case 5:
                        e_spawn = GameObject.Find("EnemySpawnMachine").GetComponent<EnemySpawnMachine>();
                        List<LinearBezier> enemyZones = new List<LinearBezier>();
                        enemyZones.Add(new LinearBezier(new Vector2(7.0f, 2.5f), new Vector2(7.0f, -2.5f)));
                        enemyZones.Add(new LinearBezier(new Vector2(-4.75f, 4f), new Vector2(-2.3f, 4f)));
                        e_spawn.setMachine(enemyZones);

                        p_spawn = GameObject.Find("PowerupSpawnMachine").GetComponent<PowerupSpawnMachine>();
                        List<Rect> powerupZones = new List<Rect>();
                        powerupZones.Add(new Rect(-4.3f, -2.8f, 3f, 4f));
                        powerupZones.Add(new Rect(1.5f, -1.1f, 3f, 1.2f));
                        p_spawn.setMachine(powerupZones, 5.0f, 10.0f);
                        break;

                    default: break;
                }*/
            }
            else if(level == 3)
				spriteRenderer.enabled = true ;
			else 
				spriteRenderer.enabled = false ;
        }
    }
	

	public void damage_UQAC(int Strength)
	{
		HealthUQAC -= Strength*0.75f;
		Debug.Log (HealthUQAC);

		if(HealthUQAC <= MaxHealthUQAC*0.90 && HealthUQAC > MaxHealthUQAC*0.75)
			spriteRenderer.sprite = sprite_Niveau1;
		else if(HealthUQAC <= MaxHealthUQAC*0.75 && HealthUQAC > MaxHealthUQAC*0.60)
			spriteRenderer.sprite = sprite_Niveau2;
		else if(HealthUQAC <= MaxHealthUQAC*0.60 && HealthUQAC > MaxHealthUQAC*0.45)
			spriteRenderer.sprite = sprite_Niveau3;
		else if(HealthUQAC <= MaxHealthUQAC*0.45 && HealthUQAC > MaxHealthUQAC*0.30)
			spriteRenderer.sprite = sprite_Niveau4;
		else if(HealthUQAC <= MaxHealthUQAC*0.30 && HealthUQAC > MaxHealthUQAC*0.15)
			spriteRenderer.sprite = sprite_Niveau5;
		else if(HealthUQAC <= MaxHealthUQAC*0.15)
			spriteRenderer.sprite = sprite_Niveau6;

        if (HealthUQAC <= 0)
            StartCoroutine(changescene(FadeoutSpeed));

    }

    void OnGUI()
    {
        alpha += FadeoutSpeed * fadedirection * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = depth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FadeoutTexture);
    }

    IEnumerator changescene(float time)
    {
        fadedirection = 1;
        yield return new WaitForSeconds(time);
        Application.LoadLevel("Gameover");
    }

}
