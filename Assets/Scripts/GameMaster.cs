using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip eggCrackSound;
    public AudioClip successSound;
    public ProgressBar progressBar;
    public Image animeGirl;
    public TMP_Text moodText;
    public TMP_Text nameText;
    public int talkActions = 0;
    public int weedActions = 0;
    public int waterActions = 0;
    public List<Button> actionButtons;
    public Sprite egg;
    public Sprite hatch1;
    public Sprite hatch2;
    public Sprite hatch3;
    public Sprite end1n1;
    public Sprite end1n2;
    public Sprite end1n3;
    public Sprite end2n1;
    public Sprite end2n2;
    public Sprite end2n3;
    public Sprite end3n1;
    public Sprite end3n2;
    public Sprite end3n3;
    public GameObject restartButton;

    private string fetusMood = "";
    private float disableTimer = 0;

    private const string TALK_MOOD = "Excited";
    private const string WEED_MOOD = "Confident";
    private const string WATER_MOOD = "Shy";

    private const int FETUS_NUM_ACTIONS = 4;
    private const int MAX_ACTIONS = 10;
    private const float DISABLE_DURATION_SEC = 0.8f;
    void Start()
    {
        nameText.text = StartButton.animeGirlName;
        if (nameText.text == null)
        {
            nameText.text = "Jimmy Bob";
        }
        restartButton.SetActive(false);
        animeGirl.sprite = egg;
    }

    // Update is called once per frame
    void Update()
    {
        if (disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
            if (disableTimer <= 0)
            {
                disableTimer = 0;
                foreach (Button button in actionButtons)
                {
                    button.interactable = true;
                }
            }
        }
    }

    public void onActionTaken()
    {
        disableButtons();
        progressBar.progress = (float)numActions() / (float)MAX_ACTIONS;
        moodText.text = "Mood: " + mood();
        if (numActions() == FETUS_NUM_ACTIONS)
        {
            audioSource.PlayOneShot(eggCrackSound);
            fetusMood = mood();
            if (fetusMood == TALK_MOOD)
            {
                animeGirl.sprite = hatch1;
            }
            else if (fetusMood == WEED_MOOD)
            {
                animeGirl.sprite = hatch2;
            }
            else
            {
                animeGirl.sprite = hatch3;
            }
        }
        else if (numActions() >= MAX_ACTIONS)
        {
            audioSource.PlayOneShot(successSound);
            foreach (Button button in actionButtons)
            {
                button.gameObject.SetActive(false);
            }
            restartButton.SetActive(true);
            if (fetusMood == TALK_MOOD)
            {
                if (mood() == TALK_MOOD)
                {
                    animeGirl.sprite = end1n1;
                }
                else if (mood() == WEED_MOOD)
                {
                    animeGirl.sprite = end1n2;
                }
                else
                {
                    animeGirl.sprite = end1n3;
                }
            }
            else if (fetusMood == WEED_MOOD)
            {
                if (mood() == TALK_MOOD)
                {
                    animeGirl.sprite = end2n1;
                }
                else if (mood() == WEED_MOOD)
                {
                    animeGirl.sprite = end2n2;
                }
                else
                {
                    animeGirl.sprite = end2n3;
                }
            }
            else
            {
                if (mood() == TALK_MOOD)
                {
                    animeGirl.sprite = end3n1;
                }
                else if (mood() == WEED_MOOD)
                {
                    animeGirl.sprite = end3n2;
                }
                else
                {
                    animeGirl.sprite = end3n3;
                }
            }
        }
    }
    private string mood()
    {
        if (talkActions == weedActions && weedActions == waterActions)
        {
            return "CONTENT";
        }
        else if (talkActions > weedActions && talkActions > waterActions)
        {
            return TALK_MOOD;
        }
        else if (weedActions > talkActions && weedActions > waterActions)
        {
            return WEED_MOOD;
        }

        return WATER_MOOD;
    }

    private void disableButtons()
    {
        foreach (Button button in actionButtons)
        {
            button.interactable = false;
        }

        disableTimer = DISABLE_DURATION_SEC;
    }

    private int numActions()
    {
        return talkActions + weedActions + waterActions;
    }
    public void talk()
    {
        if (numActions() < MAX_ACTIONS)
        {
            talkActions++;
            onActionTaken();
        }
    }
    
    public void weed()
    {
        if (numActions() < MAX_ACTIONS)
        {
            weedActions++;
            onActionTaken();
        }
    }

    public void water()
    {
        if (numActions() < MAX_ACTIONS)
        {
            waterActions++;
            onActionTaken();
        }
    }

    public void onRestart()
    {
        SceneManager.LoadScene("NamingScene");        
    }
}
