using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    public GameObject triviaUI;
    public bool isTriviaShown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
