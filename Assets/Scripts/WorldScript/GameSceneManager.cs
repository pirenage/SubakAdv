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
    string currentScene;
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
        yield return new WaitForEndOfFrame();

        screenTint.UnTint();
    }
    public void SwitcScene(string to, Vector3 targertposition)
    {
        SceneManager.LoadScene(to, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;

        Transform playerTransform = GameManager.instance.player.transform;

        CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();

        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(playerTransform, targertposition - playerTransform.position);

        GameManager.instance.player.transform.position = new Vector3(targertposition.x, targertposition.y, playerTransform.position.z);
    }
}
