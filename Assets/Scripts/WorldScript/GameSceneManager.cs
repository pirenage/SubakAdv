using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public static GameSceneManager instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] ScreenTint screenTint;
    //[SerializeField] CameraConfiner confiner;
    string currentScene;
    AsyncOperation load;
    AsyncOperation unload;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void InitSwitchScreen(string to, Vector3 targetposition)
    {
        StartCoroutine(Transition(to, targetposition));
    }

    IEnumerator Transition(string to, Vector3 targetposition)
    {
        screenTint.Tint();
        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);
        SwitcScene(to, targetposition);
        while (load != null && unload != null)
        {
            if (load.isDone) { load = null; }
            if (unload.isDone) { unload = null; }
        }
        yield return new WaitForSeconds(0.1f);

        screenTint.UnTint();
        //confiner.UpdateBounds();
    }
    public void SwitcScene(string to, Vector3 targertposition)
    {
        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;

        Transform playerTransform = GameManager.instance.player.transform;

        CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();

        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(playerTransform, targertposition - playerTransform.position);

        GameManager.instance.player.transform.position = new Vector3(targertposition.x, targertposition.y, playerTransform.position.z);
    }
}
