using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // Fireballs
    public float[] fireballSpeed = { 2.5f, -2.5f};  // How fast the balls will be rotating around
    public float distance = 0.25f;                  // How far away will they be from the boss
    public Transform[] fireballs;                   // Fill in this array with the balls

    private void Update()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance, Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0);
        }
    }
}
