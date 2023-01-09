using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerShooting : NetworkBehaviour
{
    [SerializeField] Transform gunBarrell;
    [SerializeField] TrailRenderer bulletTrail;


    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ShootServerRpc();
            }
        }
    }

    [ServerRpc]
    void ShootServerRpc()
    {
        if(Physics.Raycast(gunBarrell.position, gunBarrell.forward, out RaycastHit hit, 200f))
        {
            var enemyHealth = hit.transform.GetComponent<PlayerHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(10);
            }
        }
        ShootClientRpc();
    }

    [ClientRpc]
    void ShootClientRpc()
    {
        var bullet = Instantiate(bulletTrail, gunBarrell.forward, Quaternion.identity);
        bullet.AddPosition(gunBarrell.position);
        if (Physics.Raycast(gunBarrell.position, gunBarrell.forward, out RaycastHit hit, 200f))
        {
            bullet.transform.position = hit.point;
        }
        else
        {
            bullet.transform.position = gunBarrell.position + (gunBarrell.forward * 200f);
        }
    }
}
