using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartButton : MonoBehaviour
{
    public TMP_Text nameText;

    public static string animeGirlName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        if (nameText.text.Length > 0)
        {
            animeGirlName = nameText.text;
            SceneManager.LoadScene("MainScene");
        }
    }
}
