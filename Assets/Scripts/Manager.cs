using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    InputKey activateInput;
    [SerializeField]
    LetterAtATime activateLettering;
    [SerializeField]
    Animator tabTransition;
    [SerializeField]
    Animator cameraAnimations;
    [SerializeField]
    Transform selectedTabIndicator;
    [SerializeField]
    Text screenText;
    [SerializeField]
    TextManager textManager;
    [SerializeField]
    Camera cameraMain;
    [SerializeField]
    RectTransform exitPanel;
    [SerializeField]
    Text timeProg;

    int currentPosition;
    List<float> tabPositions = new List<float>() {-7.54f, -5.84f, -3.73f, -1.93f, -0.23f};
    List<string> tabNames = new List<string>() {"Home", "Education", "Profile", "Hobbies", "Social"};
    bool transitioning;
    float scrollSpeed = 0.2f;
    RectTransform screenTextPosition;
    bool canScroll;
    Vector2 initialTextPosition = Vector2.zero;
    float scrollLength = 16.8f;
    int maxLines;
    bool exiting, unexiting;

    // Start is called before the first frame update
    void Start()
    {
        cameraMain.aspect = 16f / 9f;
        currentPosition = 0;
        selectedTabIndicator.position = new Vector3(tabPositions[currentPosition], selectedTabIndicator.position.y, 0);
        transitioning = false;
        screenTextPosition = screenText.GetComponent<RectTransform>();
        DisableScroll();
        initialTextPosition = screenTextPosition.anchoredPosition;
        maxLines = textManager.MaxScroll(tabNames[currentPosition]);
        exiting = false;
        unexiting = false;
        Cursor.visible = false;

        activateLettering.Startup();
        activateInput.Startup();                
    }

    public void Startup()
    {
        activateLettering.ChangeOfSection(tabNames[currentPosition]);
    }

    public void EnableScroll()
    {
        canScroll = true;
    }

    public void DisableScroll()
    {
        canScroll = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            activateInput.AcceptInput();
        } else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            screenTextPosition.anchoredPosition = initialTextPosition;
            activateInput.DenyInput();
        }*/

        if (!exiting)
        {
            Animations();

            TextScroll();
        }

        timeProg.text = System.DateTime.Now.ToString("HH:mm");

        Exit();
    }

    void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && exiting)
        {
            exiting = false;
            unexiting = true;
            activateLettering.ResumeWriting();
        } else
        {
            if (Input.GetKeyDown(KeyCode.Return) && exiting)
            {
                Debug.Log("Application terminated.");
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && !exiting)
            {
                exiting = true;
                unexiting = false;
                activateLettering.PauseWriting();
            }

            if (exiting)
            {
                Vector3 velocity = Vector3.zero;
                exitPanel.position = Vector3.SmoothDamp(exitPanel.position, new Vector3(exitPanel.position.x, 0, 0), ref velocity, 0.1f);

                if (exitPanel.position == new Vector3(exitPanel.position.x, 0, 0)) exiting = false;
            }

            if (unexiting)
            {
                Vector3 velocity = Vector3.zero;
                exitPanel.position = Vector3.SmoothDamp(exitPanel.position, new Vector3(exitPanel.position.x, 8, 0), ref velocity, 0.1f);

                if (exitPanel.position == new Vector3(exitPanel.position.x, 8, 0)) unexiting = false;
            }
        }       
    }

    void TextScroll()
    {
        if (canScroll && Input.GetKey(KeyCode.DownArrow)) screenTextPosition.Translate(Vector3.up * scrollSpeed);
        else if (canScroll && Input.GetKey(KeyCode.UpArrow)) screenTextPosition.Translate(-1*Vector3.up * scrollSpeed);
        else if (canScroll) screenTextPosition.Translate(-1*Vector3.up*Input.mouseScrollDelta.y*scrollSpeed);

        if (screenTextPosition.anchoredPosition.y < initialTextPosition.y) screenTextPosition.anchoredPosition = initialTextPosition;
        else if (screenTextPosition.anchoredPosition.y > initialTextPosition.y + scrollLength*maxLines && maxLines != 0) screenTextPosition.anchoredPosition = new Vector2(screenTextPosition.anchoredPosition.x, initialTextPosition.y + scrollLength*maxLines + 5f);
        else if (screenTextPosition.anchoredPosition.y > initialTextPosition.y + scrollLength * maxLines && maxLines == 0) screenTextPosition.anchoredPosition = new Vector2(screenTextPosition.anchoredPosition.x, initialTextPosition.y + scrollLength * maxLines);
    }

    void Animations()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentPosition < tabPositions.Count-1)
            {
                screenTextPosition.anchoredPosition = initialTextPosition;
                activateInput.DenyInput();
                currentPosition++;

                transitioning = true;

                activateLettering.ChangeOfSection(tabNames[currentPosition]);
                maxLines = textManager.MaxScroll(tabNames[currentPosition]);

                tabTransition.SetTrigger("Right");
            } else
            {
                cameraAnimations.SetTrigger("ImpossibleInput");
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentPosition > 0)
            {
                screenTextPosition.anchoredPosition = initialTextPosition;
                activateInput.DenyInput();
                currentPosition--;

                transitioning = true;

                activateLettering.ChangeOfSection(tabNames[currentPosition]);
                maxLines = textManager.MaxScroll(tabNames[currentPosition]);

                tabTransition.SetTrigger("Left");
            } else
            {
                cameraAnimations.SetTrigger("ImpossibleInput");
            }
        }

        if (transitioning)
        {
            Vector3 velocity = Vector3.zero;
            selectedTabIndicator.position = Vector3.SmoothDamp(selectedTabIndicator.position, new Vector3(tabPositions[currentPosition], selectedTabIndicator.position.y, 0), ref velocity, 0.1f);

            if (selectedTabIndicator.position == new Vector3(tabPositions[currentPosition], selectedTabIndicator.position.y, 0)) transitioning = false;
        }
    }
}
