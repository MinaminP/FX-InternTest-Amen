using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public GameObject transitionOut;
    public GameObject transitionIn;
    public GameObject fadeBackground;
    public CanvasGroup canvasGroup;
    public float transitionInTime, transitionOutTime;
    // Start is called before the first frame update
    void Start()
    {
        Transition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           FadeOut();
        }
    }
    void Transition()
    {
        transitionIn = GameObject.FindGameObjectWithTag("TransitionIn");
        transitionIn.LeanMoveX(-Screen.width * 2, transitionInTime);

        transitionOut = GameObject.FindGameObjectWithTag("TransitionOut");
        transitionOut.transform.position = new Vector3(-Screen.width * 2, Screen.height / 2, 0);
    }

    public void ChangeScene(string SceneName)
    {
        AudioController.PlayMusic("ClickStart");
        StartCoroutine(PlayTransition(SceneName));
    }

    IEnumerator PlayTransition(string sceneName)
    {
        transitionOut.LeanMoveX(Screen.width / 2, transitionOutTime);
        yield return new WaitForSeconds(transitionOutTime);
        SceneManager.LoadScene(sceneName);
    }

    public void FadeOut()
    {
        AudioController.PlayMusic("ExitApp");
        LeanTween.alphaCanvas(canvasGroup, 0, transitionOutTime).setOnComplete(QuitApp);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

}
