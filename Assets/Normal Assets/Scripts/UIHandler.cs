using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIHandler : MonoBehaviour {

    GameObject gun;
    WeaponControl weaponControl;
    GameObject cylinder;
    GameObject[] bulletCasings;
    GameObject uiAmmo;
    Text ammoText;
    private int inMag;
    private int magCounter;
    private int ammo;
    private int maxAmmo;
    private float fireRate;

    GameObject guy;
    Character2D character2d;
    GameObject uiHeart;
    Image heartImg;
    float heartPercent;
    GameObject uiHP;
    Text hpText;
    GameObject uiBottle;
    Text bottleText;
    private float hp;
    private float maxHP;
    private int bottles;
    private int maxBottles;

    public bool spinning;
    float timeToFire;
    Vector3 spinTime;
    private float spinTimeX; // taken from WeaponControl

    public bool reloading;
   // float timeToReload;
    Vector3 spinSpeed2; // rotation rate when reloading

    UserControl userControl;

    GameObject bulletTime;
    Image bulletTimeFill;


    void Start () {
        gun = GameObject.Find("gun");
        weaponControl = gun.GetComponent<WeaponControl>();

        cylinder = GameObject.Find("UI-Cylinder");
        bulletCasings = GameObject.FindGameObjectsWithTag("UI-Bullet");

        uiAmmo = GameObject.Find("TotalAmmo");
        ammoText = uiAmmo.GetComponent<Text>();         //
        uiHeart = GameObject.Find("Heart");
        heartImg = uiHeart.GetComponent<Image>();       //
        uiHP = GameObject.Find("HP");
        hpText = uiHP.GetComponent<Text>();             //
        uiBottle = GameObject.Find("BottleText");
        bottleText = uiBottle.GetComponent<Text>();     //

        magCounter = 6;
        inMag = weaponControl.inMag;
        ammo = weaponControl.ammo;
        maxAmmo = weaponControl.maxAmmo;
        fireRate = weaponControl.fireRate;

        guy = GameObject.Find("Guy");
        character2d = guy.GetComponent<Character2D>();
        hp = character2d.health;
        maxHP = character2d.maxHealth;
        bottles = character2d.bottles;
        maxBottles = character2d.maxBottles;

        spinning = false;
        reloading = false;

        //spinTime = new Vector3(0, 0, 1.2245f); // Find better way to get accurate rotation
        spinTime = new Vector3(0, 0, 1.0f);

        ammoText.text = "" + ammo;
        bottleText.text = "" + bottles;
        hpText.text = "" + hp;

        heartImg.fillAmount = hp;

        userControl = guy.GetComponent<UserControl>();

        bulletTime = GameObject.Find("UITimer");
        bulletTimeFill = bulletTime.GetComponent<Image>();
    }
	
	void Update () {

        // Rotates UI-Cylinder
        // After shooting spinning is set to true
        if (spinning)
        {
            // After shooting timeToFire is set through SetTimeToFire
            if(Time.time <= timeToFire)
            {
                cylinder.transform.Rotate(spinTime * spinTimeX);
            }
            // after Time.Time > timeToFire spinning is set to false
            else {
                Mathf.Round(cylinder.transform.rotation.eulerAngles.z);
                spinning = false;
            }
        }

        // While Reloading, rotates UI magazine
        if (reloading)
        {
            //if(Time.time <= timeToReload)
            //{
                cylinder.transform.Rotate(new Vector3(0,0,-2f));
                if(cylinder.transform.rotation.eulerAngles.z >= -0.55f && cylinder.transform.rotation.eulerAngles.z <= 0.55f)
                {
                    reloading = false;
                    weaponControl.reloadDone = true;
                    AddCasing();
                    cylinder.transform.rotation = Quaternion.Euler(0, 0, 0);
                    
                }
           // }
           // else {
           //     reloading = false;
           //     weaponControl.reloadDone = true;
           //     AddCasing();
           // }
        }

        bulletTimeFill.fillAmount = userControl.GetBTime();
        
	}

    public void SetTimeToFire(float ttf,int iMag,float sr)
    {
        timeToFire = ttf;
        inMag = iMag;
        spinning = true;
        spinTimeX = sr;
        RemoveCasing();
    }

    public void SetTimeToReload(float ttr,int iMag)
    {
       // timeToReload = ttr;
        inMag = iMag;
        
    }

    public bool GetReloadDone()
    {
        return reloading;
    }

    // After shooting, a UI bullet-case is removed
    public void RemoveCasing()
    {
        print("magCounter: " + magCounter);
        bulletCasings[6 - inMag].SetActive(false);
        if (magCounter > 0) { // Remove as necessary
            magCounter--;
        }
    }

    // Re-Activates a UI bullet
    public void AddCasing()
    {
        foreach (GameObject i in bulletCasings)
        {
            i.SetActive(true);

        }
        magCounter++;
    }

    public void UpdateAmmo(int b)
    {
        ammoText.text = "" + b;
    }

    public void UpdateHealth(int p)
    {
        hp += p;
        hpText.text = "" + hp;
        heartPercent = hp / 100;
        print(heartPercent);
        heartImg.fillAmount = heartPercent;
    }

}
