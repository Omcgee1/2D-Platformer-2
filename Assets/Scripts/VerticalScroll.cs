using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    float scrollRate = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * scrollRate * Time.deltaTime);
    }
}
