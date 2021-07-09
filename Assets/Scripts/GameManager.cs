using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private string userString;
    private string generatedString;
    public bool submit;
    public Text UString;
    public Text GString;
    public GameObject keyPad;
    public GameObject Timer;
    public bool match;
    public GameObject Click;
    public GameObject Matched;
    public GameObject UnMatched;


    private void Start()
    {
        match = false;
        submit = false;
    }
    public void UpdateString(GameObject obj)
    {
        string name = obj.name;

        if (name != "Enter" && name != "Back")
        {
            Click.GetComponent<AudioSource>().Play(0);
            userString += name;
            RenderString(UString,userString);
        }
        else if(name == "Enter")
        {
            if (!Timer.GetComponent<Timer>().timerEnd)
            {
                submit = true;
                MatchString();
            }
        }
        else if(name == "Back")
        {
            if (userString.Length > 0)
            {
                Click.GetComponent<AudioSource>().Play(0);
                userString = userString.Substring(0, userString.Length - 1);
                RenderString(UString, userString);
            }
        }
    }

    public void RenderString(Text t,string tx)
    {
        t.text = tx;
    }

    public void GenerateString()
    {
        generatedString = "";
        int no = Random.Range(4, 7);
        for(int i = 0; i < no; i++)
        {
            generatedString += ((int) Random.Range(0, 9)).ToString();
        }

    }

    private void Update()
    {
        if((Timer.GetComponent<Timer>().timerEnd && !submit) || (submit && !match))
        {
            keyPad.GetComponent<Image>().color = Color.red;
            
        }
        else if(submit && match)
        {
            keyPad.GetComponent<Image>().color = Color.green;
        }
        else
        {
            keyPad.GetComponent<Image>().color = Color.white;
        }
    }

    public void GPipeline()
    {
        GenerateString();
        RenderString(GString, generatedString);
    }

    public void MatchString()
    {
        if (generatedString == userString)
        {
            match = true;
            Matched.GetComponent<AudioSource>().Play(0);
        }
        else
        {
            match = false;
            UnMatched.GetComponent<AudioSource>().Play(0);
        }
    }

    public void UPipeline()
    {
        userString = "";
        RenderString(UString,userString);
    }
}
