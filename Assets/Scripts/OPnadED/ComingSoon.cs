using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ComingSoon : MonoBehaviour
{
    private bool isFadingPart = true;
    private bool isCutInPart = false;
    private bool isEndrollOPart = false;

    private bool isFadingIn = true;
    private bool isWaiting = false;
    private bool isFadingOut = false;

    private float waitingTime = 0;

    private SpriteRenderer blinder;
    private SpriteRenderer backImage;
    private VideoPlayer videoPlayer;

    private AudioSource audioSource;

    public void Start()
    {
        blinder = GameObject.Find("BackGround").GetComponent<SpriteRenderer>();
        blinder.color = new Color (0, 0, 0, 1);

        backImage = GameObject.Find("BackImage").GetComponent<SpriteRenderer>();

        videoPlayer = GameObject.Find("EndrollMove").GetComponent<VideoPlayer>();

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
    }

    public void Update()
    {
        if (isFadingPart)
        {
            if (isFadingIn)
            {
                blinder.color -= new Color (0, 0, 0, Time.unscaledDeltaTime/2);
                audioSource.volume += Time.unscaledDeltaTime/2;
            
                if (blinder.color.a <= 0)
                {
                    isFadingIn = false;
                    isWaiting = true;
                }
            }

            if (isWaiting)
            {
                waitingTime += Time.deltaTime;

                if (waitingTime >= 1.5)
                {
                    videoPlayer.Play();

                    waitingTime = 0;

                    isWaiting = false;
                    isFadingOut = true;
                }
            }

            if (isFadingOut)
            {
                backImage.color -= new Color (0, 0, 0, Time.unscaledDeltaTime/2);

                waitingTime += Time.deltaTime;

                if (backImage.color.a <= 0)
                {
                    isFadingOut = false;
                    isWaiting = true; 

                    isFadingPart = false;
                    isEndrollOPart = true;
                }
            }
        }

        if (isEndrollOPart)
        {
            if (isWaiting)
            {
                waitingTime += Time.deltaTime;

                if (waitingTime >= 43)
                {
                    isWaiting = false;
                    isFadingOut = true;
                }
            }

            if (isFadingOut)
            {
                blinder.color += new Color (0, 0, 0, Time.unscaledDeltaTime/2);
                audioSource.volume -= Time.unscaledDeltaTime/2;

                if (blinder.color.a >= 1)
                {
                    SceneManager.LoadScene("Home");
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            isEndrollOPart = true;
            isWaiting = false;
            isFadingOut = true;
        }
    }
}