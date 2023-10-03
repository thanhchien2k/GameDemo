using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderPlayer : MonoBehaviour
{
    private Vector2 screenBounds;
    private float playerWidth = 0.5f;
    private float playerHeight =0.7f;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //playerWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        //playerHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    private void LateUpdate()
    {   
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + playerWidth, screenBounds.x - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + playerHeight - 0.2f, screenBounds.y - playerHeight);
        transform.position = viewPos;
    }
}
