using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int timer = 10;
    private int current;
    public bool timerEnd;
    public GameObject GameManager;
    public GameObject UnMatched;

    // Start is called before the first frame update
    void Start()
    {
        timerEnd = false;
        gameObject.GetComponent<Text>().text = timer.ToString();
        StartTimer();
    }

    // Update is called once per frame
    
    void StartTimer()
    {
        current = timer+1;
        timerEnd = false;
        GameManager.GetComponent<GameManager>().GPipeline();
        GameManager.GetComponent<GameManager>().UPipeline();
        GameManager.GetComponent<GameManager>().submit = false;
        GameManager.GetComponent<GameManager>().match = false;
 
        InvokeRepeating("UpdateTimer", 0f, 1.0f);
    }
    void UpdateTimer()
    {
        if (current > 0 && !GameManager.GetComponent<GameManager>().submit)
        {
            current-= 1;
            gameObject.GetComponent<Text>().text = current.ToString();
        }
        else if(current == 0 && !GameManager.GetComponent<GameManager>().submit)
        {
            UnMatched.GetComponent<AudioSource>().Play(0);
            timerEnd = true;
            GameManager.GetComponent<GameManager>().submit = true;
        }
        else
        {
            timerEnd = true;
            CancelInvoke("UpdateTimer");
            Invoke("StartTimer", 2f);
            
        }
    }
}
