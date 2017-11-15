using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleScript : MonoBehaviour {


	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            // Explodes for small area  2f stun

        } else if(col.gameObject.tag == "enemy")
        {
            // 1f stun

        } else
        {
            Destroy(this.gameObject);
        }
    }
}
