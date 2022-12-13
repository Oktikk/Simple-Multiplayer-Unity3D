using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] GameObject _ConnectionPanel;
    public void Host()
    {
        _ConnectionPanel.SetActive(false);
        NetworkManager.Singleton.StartHost();
    }

    public void Client()
    {
        _ConnectionPanel.SetActive(false);
        NetworkManager.Singleton.StartClient();
    }
}
