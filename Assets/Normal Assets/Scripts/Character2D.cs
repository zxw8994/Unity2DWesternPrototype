using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : MonoBehaviour {

    //public int maxHealth = 20;
    public int maxHealth;
    public int health;
    public int maxBottles;
    public int bottles;

    GameObject canvas;
    UIHandler uiHandler;

    void Start () {
        maxHealth = 100;
        maxBottles = 3;
        bottles = 0;

        health = maxHealth;

        canvas = GameObject.Find("Canvas");
        uiHandler = canvas.GetComponent<UIHandler>();
	}
	
	/*void Update () {

    }*/

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        // Make sure that when updating health with dmg to make it a negative
        uiHandler.UpdateHealth(-dmg); 
    }

    /*void OnCollisionEnter2D(Collision2D col)
    {
        
    }*/

}
