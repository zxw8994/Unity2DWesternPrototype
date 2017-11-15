using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour {

    public LayerMask ToHit; // LayerMask to get layers that would be targeted by Raycast

    public float fireRate;
    public float fireRate2;
    private float timeToFire = 0;
    private float timeToFire2 = 0;
    private float spinTime;
    private float spinTime2;
    Transform firePoint;
    public bool bursting = false;
    int i = 0;
    private bool reloading;
    public bool reloadDone;
    public float reloadTime;
    private float timeToReload;

    public int maxAmmo = 36;   // Max ammo
    public int ammo = 18;           // Extra bullets
    public int maxInMag = 6;   // 
    public int inMag;          // Ammo in mag

    public int maxBottles;
    public int bottles;

    public int dmg = 5;
    public Rigidbody2D bullet;
    public float maxRange = 100;

    public float accRangeMin = 0;
    public float accRangeMax = 0;
    public float recoilRate = 5f;
    public float aimStabilizeRate = 0.8f;

    public Rigidbody2D bottle;

    GameObject uiCanvas;
    UIHandler uiHandler;

    Vector2 mosPos;
    Vector2 firePointPos;


    void Start () {

        uiCanvas = GameObject.Find("Canvas");
        uiHandler = uiCanvas.GetComponent<UIHandler>();

        firePoint = transform.FindChild("muzzle");
        if(firePoint == null)
        {
            Debug.LogError("No Muzzle");
        }

        inMag = maxInMag;
        print(inMag);
        fireRate = 0.6f;
        fireRate2 = 0.05f;
        spinTime = 1.6235f;
        spinTime2 = 15.0f;
        reloading = false;
        reloadDone = false;
        reloadTime = 2f;

        maxBottles = 3;
        bottles = 0;

    }
	
	void Update () {

        if (!reloading && Time.time > timeToFire)
        {
            if (inMag > 0)
            {
                if (!bursting)
                {
                    if (Input.GetMouseButtonDown(0) )
                    {
                        timeToFire = Time.time + fireRate;
                        uiHandler.SetTimeToFire(timeToFire, inMag,spinTime);
                       // uiHandler.spinning = true;
                        SingleShot();
                        //uiHandler.SpinCylinder(timeToFire);

                    }
                }
                if (Input.GetMouseButton(1))
                {
                    //timeToFire2 = Time.time + fireRate2;
                    //FullShot();
                    bursting = true;
                }
                //else { bursting = false; }
            }
            if (inMag != maxInMag) {
                if (Input.GetKeyDown("r"))
                {
                    timeToReload = Time.time + reloadTime;
                    reloading = true;
                    uiHandler.SetTimeToReload(timeToReload, inMag);
                    uiHandler.reloading = true;
                    //ReloadGun();
                }
            }

            if (!bursting)
            {
                if (Input.GetKeyDown("f"))
                {
                    Throw();
                }
            }
            if (bursting)
            { 
                if (Time.time > timeToFire2)
                {
                    timeToFire2 = Time.time + fireRate2;
                    uiHandler.SetTimeToFire(timeToFire2, inMag, spinTime2);
                    FullShot();
                }
                if (inMag == 0)
                {
                    bursting = false;
                }
            }

        }

        if (reloading)
        {

            
            if (reloadDone)
            {
                print("Done Reloading");
                ReloadGun();
                print(inMag);
                reloading = false;
                reloadDone = false;
            }
        }

        mosPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPos, mosPos - firePointPos, maxRange, ToHit);
        Debug.DrawLine(firePointPos, mosPos);

        if (accRangeMax > 0 && accRangeMin < 0) {
            accRangeMax -= 0.02f;
            accRangeMin += 0.02f;
            print(accRangeMin + " - " + accRangeMax);
        }



    }

    void SingleShot()
    {
        //i++;
        //print(i);

        //Vector2 mosPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //Vector2 firePointPos = new Vector2(firePoint.position.x,firePoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast(firePointPos,mosPos - firePointPos,maxRange,ToHit);
        //Debug.DrawLine(firePointPos, mosPos);
        // Can Use for Enemies??
        /*if(hit.collider != null)
        {

        }*/
        Rigidbody2D clone;
        clone = Instantiate(bullet, firePoint.position /*+ (10 * firePoint.transform.forward)*/, firePoint.rotation);
        //clone = Instantiate(bullet,firePoint.position,)
        //clone.velocity = firePoint.transform.TransformDirection(new Vector2(mosPos.x - firePointPos.x, mosPos.y - firePointPos.y) * 5);
        // clone.velocity = new Vector2(mosPos.x - firePointPos.x, mosPos.y - firePointPos.y) * 5;
        //clone.velocity = new Vector2(mosPos.x - firePointPos.x, mosPos.y - firePointPos.y).normalized * 35;
        clone.velocity = new Vector2((mosPos.x - firePointPos.x) + Random.Range(accRangeMin, accRangeMax), (mosPos.y - firePointPos.y) + Random.Range(accRangeMin, accRangeMax)).normalized * 35;
        inMag--;
        print(inMag);
    }

    void FullShot()
    {
        Rigidbody2D clone;
        clone = Instantiate(bullet, firePoint.position , firePoint.rotation);
        clone.velocity = new Vector2((mosPos.x - firePointPos.x) + Random.Range(accRangeMin, accRangeMax), (mosPos.y - firePointPos.y) + Random.Range(accRangeMin,accRangeMax)).normalized * 35;
        accRangeMin = accRangeMin - 0.5f;
        accRangeMax = accRangeMax + 0.5f;

        inMag--;
        print(inMag);
    }

    void ReloadGun()
    {
        if (ammo > 0)
        {
            ammo -= maxInMag - inMag;
            if (ammo <= 0)
            {
                inMag += (maxInMag - inMag) + ammo;
                ammo -= ammo;
            }
            else
            {
                inMag += (maxInMag) - inMag;
            }
        }
        uiHandler.UpdateAmmo(ammo);
        print(ammo);
    }

    void Throw()
    {
        Rigidbody2D clone;
        clone = Instantiate(bottle,firePoint.position,firePoint.rotation);
        clone.velocity = new Vector2(mosPos.x - firePointPos.x, mosPos.y - firePointPos.y).normalized * 9;

    }

}
