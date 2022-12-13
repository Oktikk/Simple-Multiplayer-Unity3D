using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 campos;
    [SerializeField] Vector3 camposactual;

    // Update is called once per frame
    void Update()
    {
        camposactual = transform.position;
        transform.position = player.transform.position + campos;
    }
}
