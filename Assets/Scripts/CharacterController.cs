using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    
    public float m_RotateSpeed=10.0f;
    public float m_PlayerSpeed = 5.0f;
    public int m_Damage = 5;
    public float m_TimePerAttack = 1.0f;
    public float m_AttackLength = 1.5f;

    private Powerup p_PlayerPowerUp=new Mine();
    private float p_TimeUntilNextAttack = 0.0f;
    private Vector3 p_WeaponTranslation;
    private Vector3 p_WeaponOriginialRelativeLocation;
    private bool p_collision=false;


    private int maskToIgnore;

	// Use this for initialization
	void Start () {
        p_WeaponOriginialRelativeLocation = GameObject.Find("Weapon_Sprite").transform.localPosition;
        UpdateInventory();
        maskToIgnore = (1 << 0);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateInventory();
        UseItem();
	}

    void FixedUpdate()
    {
        Move();
        Attack();
    }


    private void UpdateInventory()
    {
        if (p_PlayerPowerUp!=null) { 
            GameObject.FindObjectOfType<Inventory>().myPowerup = p_PlayerPowerUp;
            //Debug.Log(GameObject.FindObjectOfType<Inventory>().myPowerup.ToString());
        }
    }


    private void Move(){

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {

            float horizontalInput=Input.GetAxis("Horizontal");
            float verticalInput=Input.GetAxis("Vertical");


            Vector3 translation = new Vector3(horizontalInput,verticalInput, 0f);
            Quaternion rotation = Quaternion.EulerRotation(0f, 0f, Mathf.Atan2(verticalInput, horizontalInput));

            //transform.position += ((Vector3.right * translation[0] + Vector3.up * translation[1]) * m_PlayerSpeed * Time.deltaTime);

            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

            RaycastHit2D hitAtPosition;

            Vector2 rayCastPosition = new Vector2(currentPosition.x + horizontalInput * m_PlayerSpeed/3, currentPosition.y + verticalInput * m_PlayerSpeed/3);
            var offset = rayCastPosition - currentPosition;
            hitAtPosition = Physics2D.Raycast(currentPosition, offset.normalized, offset.sqrMagnitude, maskToIgnore);
            Debug.DrawRay(currentPosition, offset, Color.red, 2.0f, true);
            //Debug.Log(GameObject.Find("Personnage").layer);
            
            

            if (hitAtPosition.collider==null)
            {
                    transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3.right * translation[0] + Vector3.up * translation[1]), m_PlayerSpeed * Time.deltaTime);
         
            }
            else
            {
                p_collision = true;
                transform.position = transform.position;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, m_RotateSpeed * Time.deltaTime);

            
            

        }


    }


    private void Attack()
    {

        GameObject weaponObject = GameObject.Find("Weapon_Sprite");
       

        if (Input.GetButtonDown("Fire1") && p_TimeUntilNextAttack<0.0f)
        {
            float angle = weaponObject.transform.localEulerAngles.z;
            p_WeaponTranslation = RotateAroundAxis(new Vector3(0.0f, 1.0f, 0f), angle, new Vector3(0, 0, 1), false);

            p_TimeUntilNextAttack=m_TimePerAttack;
        }

        else if (p_TimeUntilNextAttack > m_TimePerAttack/2 && p_TimeUntilNextAttack< m_TimePerAttack)
        {
            weaponObject.transform.localPosition += Vector3.right * p_WeaponTranslation[0] * Time.deltaTime * m_AttackLength;
            weaponObject.transform.localPosition += Vector3.up * p_WeaponTranslation[1] * Time.deltaTime * m_AttackLength;
        }
        else if (p_TimeUntilNextAttack < m_TimePerAttack/2 && p_TimeUntilNextAttack>0.0f)
        {
            weaponObject.transform.localPosition += Vector3.right * -p_WeaponTranslation[0] * Time.deltaTime * m_AttackLength;
            weaponObject.transform.localPosition += Vector3.up * -p_WeaponTranslation[1] * Time.deltaTime * m_AttackLength;
        }
        p_TimeUntilNextAttack -= Time.deltaTime;

    }



    private Vector3 RotateAroundAxis(Vector3 v, float a, Vector3 axis, bool bUseRadians = false)
    {
        if (bUseRadians) a *= Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(a, axis);
        return q * v;
    }

    private void UseItem()
    {
        if(Input.GetButtonDown("Fire2") && p_PlayerPowerUp!=null){
            GameObject.FindObjectOfType<Inventory>().ActivatePowerup(GameObject.FindObjectOfType<Inventory>().GetTrapNumber());
            p_PlayerPowerUp = null;
        }
    }

    
}
