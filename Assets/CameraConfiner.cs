using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraConfiner : MonoBehaviour
{
    [SerializeField] CinemachineConfiner confiner;
    private void Start()
    {
        UpdateBounds();
    }

    public void UpdateBounds()
    {
        GameObject go = GameObject.Find("CameraConfiner");
        if (go == null)
        {
            confiner.m_BoundingShape2D = null;
            return;
        }
        Collider2D bounds = GameObject.Find("CamerConfiner").GetComponent<Collider2D>();
        confiner.m_BoundingShape2D = bounds;
    }
}
