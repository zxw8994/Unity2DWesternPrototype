using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour {

    private GameObject Guy;
    private Rigidbody2D rigid;
    
    public bool jump = false;
    private float speed;
    public float normalSpeed;
    public float crouchSpeed;
    public float jumpSpeed;
    bool crouched = false;

    public bool slow;
    private float maxBulletTime = 1.5f;
    private float bulletTimePercent;
    private float currentBulletTime = 1.5f;

    Character2D char2D;

    GameObject theGun;
    WeaponControl weaponControl;


    void Start () {
        rigid = GetComponent<Rigidbody2D>();

        char2D = GetComponent<Character2D>();

        theGun = GameObject.Find("gun");
        weaponControl = theGun.GetComponent<WeaponControl>();

        crouchSpeed = 0.3f;
        normalSpeed = 0.5f;
        jumpSpeed = 0.3f;

        slow = false;
    }
	
	void Update () {

        // Base movement
        if (!jump && !weaponControl.bursting)
        {
            if (!crouched) {
                if (Input.GetKey("a"))
                {
                    rigid.velocity += new Vector2(-normalSpeed, 0);
                }
                if (Input.GetKey("d"))
                {
                    rigid.velocity += new Vector2(normalSpeed, 0);
                }
                if (Input.GetKey("w"))
                {
                    rigid.velocity += new Vector2(0, 10f);
                    jump = true;
                }
            }
            /*else    // slower movement on crouch
            {
                    if (Input.GetKey("a"))
                    {
                        rigid.velocity += new Vector2(-crouchSpeed, 0);
                    }
                    if (Input.GetKey("d"))
                    {
                        rigid.velocity += new Vector2(crouchSpeed, 0);
                    }
            }*/
        }

        // New Movement while jumping
        if (jump)
        {
            if (Input.GetKey("a"))
            {
                rigid.velocity += new Vector2(-jumpSpeed, 0);
            }
            if (Input.GetKey("d"))
            {
                rigid.velocity += new Vector2(jumpSpeed, 0);
            }
        }

        // BULLET-TIME
        if (Input.GetKeyDown("x") && !slow)
        {
            print("SLOWED");
            slow = true;
            Time.timeScale = 0.3f;
        } else if(Input.GetKeyDown("x") && slow)
        {
            print("NOT SLOWED");
            slow = false;
            Time.timeScale = 1.0f;
        }

        // For whatever reason, bullet-time recharge rate is slower if it empties
        if (slow && currentBulletTime > 0)
        {
            currentBulletTime -= Time.deltaTime;

            
        } else if (currentBulletTime <= 0)
        {
            slow = false;
            Time.timeScale = 1.0f;
            currentBulletTime = 0.01f;
            print("NOT SLOWED");
        }

        if (!slow && currentBulletTime < maxBulletTime)
        {
            currentBulletTime += (Time.deltaTime/10);
            print(currentBulletTime);
        }
        if(currentBulletTime > maxBulletTime)
        {
            currentBulletTime = maxBulletTime;
        }
        

        // Send to UIHandler
        bulletTimePercent = currentBulletTime / maxBulletTime;
    }

    // Sorts what what picked up
    void SortPickUp(string name)
    {
        switch (name)
        {
            case "AmmoSmall":

                break;
            case "AmmoLarge":
                break;
            case "BottleP":
                break;
            case "HealthSmall":
                break;
            case "HealthLarge":
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "ground")
        {
            jump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        // Will probably pick up & destroy all things colliding
        if (col.gameObject.tag == "PickUp" && Input.GetKeyDown("e"))
        {
            SortPickUp(col.gameObject.name);
            Destroy((col.gameObject));
        }
    }


    public float GetBTime()
    {
        return bulletTimePercent;
    }

}
