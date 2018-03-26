using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Transform mainCamera;
    public enum BillboardMode { lookAt, allign };
    public BillboardMode billboardMode;

    private void LateUpdate()
    {
        if (billboardMode == BillboardMode.lookAt)
            transform.LookAt(Camera.main.transform.position, -Vector3.up);
        if (billboardMode == BillboardMode.allign)
            transform.forward = mainCamera.transform.forward;
    }
}
