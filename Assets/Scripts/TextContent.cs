using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextContent : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Text textMesh;
    private void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
        
    }

    private void Start()
    {
        TimeManager.instance.LoadData();
    }

    private void Update()
    {
            
            string score;
            score = TimeManager.instance.ScoreData.currentScore.ToString("F2");
            if (TimeManager.instance.IsHome())
            {
                score = TimeManager.instance.ScoreData.highestScore.ToString("F2");

            }
            textMesh.text = score;
    }
}
