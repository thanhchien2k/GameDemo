using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    private void Update()
    {

        if (PlayerControll.isDie && TimeManager.instance.IsSave)
        {
            string score;
            score = TimeManager.instance.ScoreData.currentScore.ToString("F2");
            if (TimeManager.instance.IsHome())
            {
                score = TimeManager.instance.ScoreData.highestScore.ToString("F2");
                Debug.Log("home");

            }
            textMesh.text = score;

        }
    }
}
