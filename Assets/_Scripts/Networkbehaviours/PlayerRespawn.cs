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

        if (IsLocalPlayer)
        {
            transform.position += new Vector3(1, 0, 0);
        }
        else
        {
            transform.position -= new Vector3(1, 0, 0);
        }
    }
    [ClientRpc]
    void ShowPlayerClientRpc()
    {
        SetPlayerState(true);
        cc.enabled = true;
    }
    void RespawnClient(Vector3 spawnPos)
    {

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
        foreach(var behaviour in behaviours)
        {
            behaviour.enabled = state;
        }
        foreach(var renderer in renderers)
        {
            renderer.enabled = state;
        }
    }
}
