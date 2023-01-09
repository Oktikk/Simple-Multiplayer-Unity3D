using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerRespawn : NetworkBehaviour
{
    CharacterController cc;
    Renderer[] renderers;
    [SerializeField] Behaviour[] behaviours;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        renderers = GetComponentsInChildren<Renderer>();

    }

    private void Update()
    {
        if (IsLocalPlayer && Input.GetKeyDown(KeyCode.Y))
        {
            RespawnServerRpc();
        }
    }


    [ServerRpc]
    void RespawnServerRpc()
    {
        StartCoroutine(RespawnOnServer());
    }

    IEnumerator RespawnOnServer()
    {
        HidePlayerClientRpc();
        yield return new WaitForSeconds(3);
        ShowPlayerClientRpc();
    }

    [ClientRpc]
    void HidePlayerClientRpc()
    {
        cc.enabled = false;
        SetPlayerState(false);

        
    }
    [ClientRpc]
    void ShowPlayerClientRpc()
    {
        SetPlayerState(true);
        cc.enabled = true;
    }

    Vector3 GetRandomSpawn()
    {
        float x = Random.Range(-5f, 5f);
        float y = 2f;
        float z = Random.Range(-5f, 5f);
        return new Vector3(x, y, z);
    }

    void SetPlayerState(bool state)
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.enabled = state;
        }
        foreach (var renderer in renderers)
        {
            renderer.enabled = state;
        }
    }

    //private void Update()
    //{
    //    if (IsLocalPlayer && Input.GetKeyDown(KeyCode.Y))
    //    {
    //        RespawnServerRpc();
    //    }
    //}


    //[ServerRpc]
    //void RespawnServerRpc()
    //{
    //    RespawnClientRpc(GetRandomSpawn());
    //}

    //IEnumerator RespawnCoroutine(Vector3 spawnPos)
    //{
    //    HidePlayerClientRpc();
    //    yield return new WaitForSeconds(3);
    //    transform.position = spawnPos;
    //    ShowPlayerClientRpc();
    //}

    //[ClientRpc]
    //void RespawnClientRpc(Vector3 spawnPos)
    //{
    //    StartCoroutine(RespawnCoroutine(spawnPos));
    //}

    //[ClientRpc]
    //void HidePlayerClientRpc()
    //{
    //    cc.enabled = false;
    //    SetPlayerState(false);
    //}
    //[ClientRpc]
    //void ShowPlayerClientRpc()
    //{
    //    cc.enabled = true;
    //    SetPlayerState(true);
    //}

    //Vector3 GetRandomSpawn()
    //{
    //    float x = Random.Range(-5f, 5f);
    //    float y = 2f;
    //    float z = Random.Range(-5f, 5f);
    //    return new Vector3(x, y, z);
    //}

    //void SetPlayerState(bool state)
    //{
    //    foreach(var behaviour in behaviours)
    //    {
    //        behaviour.enabled = state;
    //    }
    //    foreach(var renderer in renderers)
    //    {
    //        renderer.enabled = state;
    //    }
    //}
}
