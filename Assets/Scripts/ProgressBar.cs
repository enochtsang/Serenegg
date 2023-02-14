using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float progress = 0;
    public Image bar;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float newProgress = progress >= 1 ? 1 : progress;
        bar.rectTransform.localScale = new Vector3(newProgress, 1, 1);
    }
}
