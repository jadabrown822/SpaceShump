using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Keeps a GameObject on screen
 * Note that this ONLY works for an orthographic Main Camera
*/

public class BoundsCheck : MonoBehaviour
{
    public enum eType { center, inset, outset }

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;


    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }


    private void LateUpdate()
    {
        // find the checkRadius that will enable center, inset, or outset
        float checkRadius = 0;
        if (boundsType == eType.inset)
        {
            checkRadius = -radius;
        }
        if (boundsType == eType.outset)
        {
            checkRadius = radius;
        }

        Vector3 pos = transform.position;
        isOnScreen = true

        // Restric the X position to camWidth
        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
            isOnScreen = false;
        }
        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
            isOnScreen = false;
        }

        // Restrict the Y position to camHeight
        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
            isOnScreen = false;
        }
        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camWidth - checkRadius;
            isOnScreen = false;
        }

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
        }
    }



    /*
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    */
}
