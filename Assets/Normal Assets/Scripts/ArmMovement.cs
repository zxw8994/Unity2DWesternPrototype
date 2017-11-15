﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour {

    public int rotOffset = 0;
    public GameObject gun;

    // Controls players arm movement
	void Update () {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //print(rotZ);
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - rotOffset);
	}
}
