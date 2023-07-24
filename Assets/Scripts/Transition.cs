using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Transitiontype
{
    Warp,
    Scene
}
public class Transition : MonoBehaviour
{
    Transform destination;
    [SerializeField] Transitiontype transitiontype;
    [SerializeField] string sceneNameTransition;

    internal void InitiateTransition(Transform toTransition)
    {
        switch (transitiontype)
        {
            case Transitiontype.Warp:
                toTransition.position = new Vector3(destination.position.x, destination.position.y, toTransition.position.z);
                break;
            case Transitiontype.Scene:
                SceneManager.LoadScene(sceneNameTransition);
                break;

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.GetChild(1);
    }


}
