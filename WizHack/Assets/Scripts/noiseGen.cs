using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noiseGen : MonoBehaviour
{
    public int pixWidth;
    public int pixHeight;

    public float xOrg;
    public float yOrg;
    public float scale = 1.0F;

    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        rend.material.mainTexture = noiseTex;
    }

    void CalcNoise()
    {
        float y = 0.0F;
        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float Xcoord = xOrg + x / noiseTex.width * scale;
                float Ycoord = yOrg + y / noiseTex.width * scale;
                float sample = Mathf.PerlinNoise(Xcoord, Ycoord);
                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                x++;
            }

            y++;
        }

        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        CalcNoise();
    }
}
