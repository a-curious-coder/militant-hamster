using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float additionalScrollSpeed;
    public GameObject[] backgrounds;
    // Scroll speed for each background
    public float[] scrollSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int b = 0; b < backgrounds.Length; b++) {
            Renderer rend = backgrounds[b].GetComponent<Renderer>();

            float offset = Time.time * (scrollSpeed[b] * additionalScrollSpeed);

            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }
}
