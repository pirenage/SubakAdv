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
    [SerializeField] Vector3 targetPosition;
    [SerializeField] AudioClip bukapintu;

    internal void InitiateTransition(Transform toTransition)
    {
        Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();

        switch (transitiontype)
        {
            case Transitiontype.Warp:
                currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition, destination.position - toTransition.position);

                toTransition.position = new Vector3(destination.position.x, destination.position.y, toTransition.position.z);
                break;
            case Transitiontype.Scene:
                // AudioManager.instance.Play(bukapintu);
                GameSceneManager.instance.InitSwitchScreen(sceneNameTransition, targetPosition);
                break;

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.GetChild(1);
    }


}
