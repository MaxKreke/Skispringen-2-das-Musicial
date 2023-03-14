using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Speedometer : MonoBehaviour
{
    private float height;

    private void Start()
    {
        height = transform.parent.GetComponent<RectTransform>().rect.height;
    }

    public void SetSpeed(float speed)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, height*(speed / 150.0f)+2.5f);
    } 
    
}
