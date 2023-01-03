using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public GameObject startButton, titleText;
    public float startMenuSFXTimer;
    // Start is called before the first frame update
    void Start()
    {
        Tweening();
        StartCoroutine(PlayMenuSound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Tweening()
    {
        LeanTween.scale(startButton, new Vector3(0.75f, 0.75f, 0.75f), 0.5f).setLoopPingPong();
        LeanTween.scale(titleText, new Vector3(1.05f, 1.05f, 1.05f), 0.5f).setLoopPingPong();
    }

    IEnumerator PlayMenuSound()
    {
        yield return new WaitForSeconds(startMenuSFXTimer);
        AudioController.PlayMusic("MenuEnter");
    }
}
