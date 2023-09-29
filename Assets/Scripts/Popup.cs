using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Popup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {  
        if (TimeManager.instance.IsHome()) transform.localScale = Vector3.one ;
        else
        {
            if (transform.localScale == Vector3.zero && PlayerControll.isDie)
            {
                PopUp();
            }
            else if (transform.localScale == Vector3.one && !PlayerControll.isDie)
            {
                PopDown();
            }
        }

    }

    private void PopUp()
    {   
    
        transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.InOutCubic);
    }

    private void PopDown()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutCubic);
    }

    public void Restart()
    {
        TimeManager.instance.RestartLevel();
    }

    public bool IsHome()
    {
        return TimeManager.instance.IsHome();
    }

    public void LoadIndexScene(int index)
    {
        TimeManager.instance.LoadScene(index);
    }
}
