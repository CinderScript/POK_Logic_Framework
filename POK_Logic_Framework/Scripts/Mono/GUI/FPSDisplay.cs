using UnityEngine;
using UnityEngine.UI;

class FPSDisplay : MonoBehaviour {

    public Text FPSText = null;

    public float updateInterval = 0.5F;

    private bool textFound = true;
    private float framesAccum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval

    void Start()
    {
        timeleft = updateInterval;

        if ( FPSText == null )
        {
            Debug.Log("No Text component linked!");
            textFound = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( textFound )
        {
            timeleft -= Time.deltaTime;
            framesAccum += Time.timeScale / Time.deltaTime;
            ++frames;

            // Interval ended - update GUI text and start new interval
            if ( timeleft <= 0.0 )
            {
                // display two fractional digits (f2 format)
                float fps = framesAccum / frames;
                string format = string.Format("{0:F2} FPS", fps);
                FPSText.text = format;

                if ( fps > 50 )
                    FPSText.color = Color.green;
                else
                    if ( fps > 40 )
                    FPSText.color = Color.yellow;
                else
                    FPSText.color = Color.red;
                //	DebugConsole.Log(format,level);
                timeleft = updateInterval;
                framesAccum = 0.0f;
                frames = 0;
            }
        }
    }
}