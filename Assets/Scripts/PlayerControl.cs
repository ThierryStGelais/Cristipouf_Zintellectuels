using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

	AudioSource audioS;

    // Normal Movements Variables
    Vector3 inputMovement;
    public float Speed, turnSpeed;
    public uint currentHealth;

	Sword itsSword;

    public Classes stats;

	Animator membres;

	float maxAnimSpeed;

    private Powerup p_PlayerPowerUp;

	public AudioClip useItem, getItem;

    void Start()
    {
		audioS = GetComponent<AudioSource>();

		maxAnimSpeed = (1.0f / 100.0f) * Speed;

		int playerType = GameControl.instance.GetComponent<GameControl>().playerType;

		itsSword = transform.FindChild("sword").GetComponent<Sword>();

		itsSword.setHolder(transform.FindChild("New Sprite").gameObject);

		membres = transform.FindChild("brasjambes").GetComponent<Animator>();

		switch (playerType)
		{
			case 0:
				itsSword.setSprite(Resources.Load<Sprite>("Sprites/Brigadiere_inGame/stop_bonnetaille"));
                GameObject.FindGameObjectWithTag("ClassText").GetComponent<Text>().text = "Brigadiere";
                GameObject.FindGameObjectWithTag("ClassImage").GetComponent<Image>().sprite= Resources.Load<Sprite>("Sprites/UI_Sprites/Brigadiere_final");
                stats = gameObject.GetComponent<class_Heavy>();
				break;
			
			case 1:
				itsSword.setSprite(Resources.Load<Sprite>("Sprites/Concierge_inGame/moppe_droite_bonnetaille"));
                GameObject.FindGameObjectWithTag("ClassText").GetComponent<Text>().text = "Concierge";
                GameObject.FindGameObjectWithTag("ClassImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI_Sprites/Concierge_final");
                stats = gameObject.GetComponent<class_Light>();

				break;
			
			case 2:
				itsSword.setSprite(Resources.Load<Sprite>("Sprites/Mecano_inGame/wrench_bonnetaille"));
                GameObject.FindGameObjectWithTag("ClassText").GetComponent<Text>().text = "Mécano";
                GameObject.FindGameObjectWithTag("ClassImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI_Sprites/mecano_final");
                stats = gameObject.GetComponent<class_Tech>();
				break;
		}

        UpdateInventory();
    }

    void Awake()
    {

    }

    void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, gameObject.transform.position.y, mousePos.z);

        // Move senteces
        inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		Vector3 finalV = inputMovement * Time.deltaTime * Speed;

        transform.Translate(finalV, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position- mousePos, Vector3.up), Time.deltaTime * turnSpeed);
        
		if (Input.GetButtonDown ("Fire1")) itsSword.Attack();

		float vSpeed = Mathf.Sqrt (Mathf.Pow (finalV.x, 2) + Mathf.Pow (finalV.y, 2) + Mathf.Pow (finalV.z, 2));

		membres.speed = vSpeed / maxAnimSpeed;

        UpdateInventory();
        UseItem();
    }

    private void UpdateInventory()
    {
        if (p_PlayerPowerUp != null)
        {
            GameObject.FindObjectOfType<Inventory>().myPowerup = p_PlayerPowerUp;
            //Debug.Log(GameObject.FindObjectOfType<Inventory>().myPowerup.ToString());
        }
    }

    private void UseItem()
    {
        if (Input.GetButtonDown("Fire2") && p_PlayerPowerUp != null)
        {
			audioS.PlayOneShot(useItem);

            GameObject.FindObjectOfType<Inventory>().ActivatePowerup(GameObject.FindObjectOfType<Inventory>().GetTrapNumber());
            p_PlayerPowerUp = null;
            GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>().text = "Rien";
            GameObject.FindGameObjectWithTag("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI_Sprites/NoEquipment");
        }
    }


    public void addItem(int traptoadd)
    {
        Debug.Log("collision");
		audioS.PlayOneShot (getItem);
        switch (traptoadd)
        {
                case 0:
                    p_PlayerPowerUp = new Mur();
                    GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>().text = "Barrière";
                    GameObject.FindGameObjectWithTag("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI_Sprites/barriere");
                    break;
                case 1:
                    p_PlayerPowerUp = new Livre();
                    GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>().text = "Livre";
                    GameObject.FindGameObjectWithTag("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI_Sprites/Book");
                    break;
                case 2:
                    p_PlayerPowerUp = new Mine();
                    GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>().text = "Mine";
                    GameObject.FindGameObjectWithTag("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI_Sprites/Mine");
                    break;
                case 3:
                    p_PlayerPowerUp = new Rateau();
                    GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>().text = "Rateau";
                    GameObject.FindGameObjectWithTag("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI_Sprites/rateau");
                    break;
                default:
                    GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>().text = "Rien";
                    GameObject.FindGameObjectWithTag("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI_Sprites/NoEquipment");
                    break;
         }
         UpdateInventory();

    }

    public bool HasPowerUp()
    {
        if (p_PlayerPowerUp != null)
        {
            return true;
        }
        return false;
    }





    void Update()
    {   
       
       GameObject.FindGameObjectWithTag("HealthBar").GetComponent<RectTransform>().localScale = new Vector3((float) stats.Health / stats.MaxHealth, 1.0f,1.0f);
       Debug.Log("currentHealth=" + stats.Health + "      maxHealth=" + stats.MaxHealth);
    }

}
