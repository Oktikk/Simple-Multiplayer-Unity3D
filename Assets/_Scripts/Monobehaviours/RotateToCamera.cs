using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private Camera MainCamera;
    void Start()
    {
        MainCamera = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 pos = MainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        transform.LookAt(MainCamera.transform.forward);
    }
}
