using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    [SerializeField] private GameObject aim;
    [SerializeField] private Camera camera;
    [SerializeField] private float offset;
    
    void Update()
    {
        transform.position = camera.WorldToScreenPoint(aim.transform.position) - Vector3.up * offset;
    }
}
