using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputKey : MonoBehaviour
{
    List<char> input;
    float blinkTime;
    bool showBar, updateText, acceptInput;
    string writtenText, blinkText;
    [SerializeField]
    Text screenText;

    public void AcceptInput()
    {
        writtenText = screenText.text;
        input.Clear();
        for (int i = 0; i < writtenText.Length; i++) input.Add(writtenText[i]);
        acceptInput = true;
    }

    public void DenyInput()
    {
        acceptInput = false;
    }

    // Start is called before the first frame update
    /*void Start()
    {
        input = new List<char>();
        blinkTime = 0.4f;
        showBar = true;
        updateText = true;

        acceptInput = false;
    }*/

    public void Startup()
    {
        input = new List<char>();
        blinkTime = 0.4f;
        showBar = true;
        updateText = true;

        acceptInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (acceptInput)
        {
            if (Input.anyKeyDown)
            {
                screenText.text = writtenText;
                Write();
                updateText = true;
            }
            else
            {
                if (updateText)
                {
                    writtenText = screenText.text;
                    blinkText = writtenText + "_";
                    updateText = false;
                    blinkTime = 0.6f;
                }

                Blink();
            }
        }
    }

    void Blink()
    {
        if (blinkTime >= 0.7f)
        {
            if (showBar)
            {
                screenText.text = blinkText;
                showBar = false;
            } else
            {
                screenText.text = writtenText;
                showBar = true;
            }

            blinkTime = 0f;
        } else
        {
            blinkTime += Time.deltaTime;
        }
    }

    void Write()
    {
        string newText = "";

        if (Input.GetKeyDown(KeyCode.Backspace) && input.Count > 0) 
        {
            input.RemoveAt(input.Count - 1);

            for (int j = 0; j < input.Count; j++)
            {
                newText += input[j];
            }

            screenText.text = newText;
        }
        else if (!Input.GetKeyDown(KeyCode.Backspace))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                input.Add('\n');

                screenText.text += "\n";
            } else
            {
                for (int i = 0; i < Input.inputString.Length; i++)
                {
                    input.Add(Input.inputString[i]);
                }

                for (int i = 0; i < input.Count; i++)
                {
                    newText += input[i];
                }

                screenText.text = newText;
            }
        }
    }
}
