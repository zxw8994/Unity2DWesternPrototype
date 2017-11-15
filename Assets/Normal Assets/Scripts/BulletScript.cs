using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public int dmg = 5;
    private GameObject muzzle;
    Vector2 spawnPoint;
    private float maxDistance = 27f;
    private float currentDistance;

	void Start () {
        muzzle = GameObject.Find("muzzle");

        spawnPoint = this.gameObject.transform.position;
    }
	

	void Update () {

        //print(Vector2.Distance(spawnPoint, this.gameObject.transform.position));
        currentDistance = Vector2.Distance(spawnPoint, this.gameObject.transform.position);

        if(currentDistance >= maxDistance)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            col.gameObject.SendMessage("TakeDamage", dmg);
        }

        if(col.gameObject.tag != "Player")
        Destroy(this.gameObject);
    }

}
