using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    [Header("Set in Inspector")]
    public GameObject POI;
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    [Header("Set Dynamically")]
    public float camZ;

    private Vector3 distance;

    // Use this for initialization
    void Start()
    {
        distance = transform.position - POI.transform.position;
    }

    private void FixedUpdate()
    {
        if (POI == null) return;
        Vector3 temp = POI.transform.position + distance;
        transform.position = Vector3.Lerp(transform.position, temp, 5.0f * Time.deltaTime);
    }
}
