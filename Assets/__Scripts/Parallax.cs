using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Inscibed")]
    public Transform playerTrans;       // The player ship
    public Transform[] panels;      // The scrilling foregrounds
    [Tooltip("Speed at which the panel move in Y")]
    public float scrollSpeed = -30;
    [Tooltip("Controls how much panels react to player movement (Default 0.25")]
    public float motionMult = 0.25f;

    private float panelHit;         // Hieght of each Panel
    private float depth;        // Depth of panels (that is pos.z)


    // Start is called before the first frame update
    void Start()
    {
        panelHit = panels[0].localScale.y;
        depth = panels[0].position.z;

        // Set initional position of panels
        panels[0].position = new Vector3(0, 0, depth);
        panels[1].position = new Vector3(0, panelHit, depth);
    }


    // Update is called once per frame
    void Update()
    {
        float tY, tX = 0;
        tY = Time.time * scrollSpeed % panelHit + (panelHit * 0.5f);

        if (playerTrans != null)
        {
            tX = -playerTrans.transform.position.x * motionMult;
            // tY += -playerTrans.transform.position.y * motionMult;
        }

        // Position panels[0]
        panels[0].position = new Vector3(tX, tY, depth);
        // Position panels[1] where needed to make a continuous starfield
        if (tY >= 0)
        {
            panels[1].position = new Vector3(tX, tY - panelHit, depth);
        }
        else
        {
            panels[1].position = new Vector3(tX, tY + panelHit, depth);
        }
    }
}
