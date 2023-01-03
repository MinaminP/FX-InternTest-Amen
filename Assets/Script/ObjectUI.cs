using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUI : MonoBehaviour
{
    public GameObject triviaUI;
    public bool isTriviaShown = false;
    // Start is called before the first frame update
    void Start()
    {
        triviaUI.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        ShowTrivia();
    }

    public void StartAnimation()
    {
        AudioController.PlayMusic("Animate");
    }

    public void ShowTrivia()
    {
        if (!isTriviaShown)
        {
            AudioController.PlayMusic("OpenTrivia");
            LeanTween.scale(triviaUI, new Vector3(0.2f, 0.2f, 0.2f), 0.5f).setEaseOutSine();
            isTriviaShown = true;
        }
    }

    public void CloseTrivia()
    {
        AudioController.PlayMusic("ExitTrivia");
        isTriviaShown = false;
        LeanTween.scale(triviaUI, new Vector3(0f, 0f, 0f), 0.5f).setEaseOutSine();
    }
}
