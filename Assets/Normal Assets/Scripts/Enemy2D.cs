using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour {

    public int maxHealth;
    private int health;

    private bool stunned;
    private float stunCurrent = 0f;
    private float stunTime;

	void Start () {
        health = maxHealth;
        stunned = false;
	}
	
	void Update () {
		
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }

        // If not stunned Enemy can act
        if (!stunned)
        {

        } else if(stunned)
        {
            // Checks if stun duration is over
            if (Time.time > stunCurrent)
            {
                stunned = false;
            }
        }

	}

    void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    void GetStunned()
    {
        stunned = true;
        stunCurrent = Time.time + stunTime;
    }

}
