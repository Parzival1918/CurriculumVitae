using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterAtATime : MonoBehaviour
{
    bool write, writting, starting, jumpLine;
    string textual, textToFetch;
    float time;
    int pos, lineCount;
    float lineLength = 16.8f; //16.5
    bool pauseWriting;

    RectTransform screenTextPosition;
    [SerializeField]
    Text screenText;
    [SerializeField]
    TextManager textManager;
    [SerializeField]
    Manager manager;

    // Start is called before the first frame update
    /*void Start()
    {
        write = false;
        writting = false;
        time = 0f;
    }*/

    public void Startup()
    {
        write = false;
        writting = false;
        time = 0f;
        screenTextPosition = screenText.GetComponent<RectTransform>();
        pauseWriting = false;

        manager.Startup();
    }

    public void PauseWriting()
    {
        pauseWriting = true;
    }

    public void ResumeWriting()
    {
        pauseWriting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (write && !writting && starting)
        {
            writting = true;
        }

        if (writting && write && !pauseWriting)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                screenText.text = textual;
                manager.EnableScroll();
                writting = false;
                write = false;
            } else
            {
                if (starting)
                {
                    textual = textManager.TextFetcher(textToFetch);
                    starting = false;
                }

                if (time >= 0.02f)
                {
                    if (lineCount > 20 && jumpLine)
                    {
                        screenTextPosition.anchoredPosition = new Vector2(screenTextPosition.anchoredPosition.x, screenTextPosition.anchoredPosition.y + lineLength);
                        jumpLine = false;
                    }

                    if (textual[pos] == '\n')
                    {
                        lineCount++;
                        jumpLine = true;
                    }

                    screenText.text += textual[pos];
                    pos++;

                    time = 0f;
                }
                else
                {
                    time += Time.deltaTime;
                }

                if (pos == textual.Length)
                {
                    manager.EnableScroll();
                    writting = false;
                }
            }
        }
    }

    void StartWritting()
    {
        screenText.text = "";
        pos = 0;
        time = 1f;
        jumpLine = true;
        starting = true;
        write = true;
        lineCount = 0;
    }

    void StopWritting()
    {
        screenText.text = "";
        writting = false;
        write = false;
    }

    public void ChangeOfSection(string a)
    {
        manager.DisableScroll();
        StopWritting();
        textToFetch = a;
        StartWritting();
    }
}
