using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerHealth : NetworkBehaviour
{
    NetworkVariable<int> health = new NetworkVariable<int>(100);

    public int actualHealth = 100;
    // Update is called once per frame
    void Update()
    {
        actualHealth = health.Value;
    }

    public void TakeDamage(int damage)
    {
        health.Value -= damage; 

        if(health.Value < 0)
        {

        }
    }
}
