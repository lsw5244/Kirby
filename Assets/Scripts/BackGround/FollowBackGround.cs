using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBackGround : MonoBehaviour
{
    public GameObject gameCamera;
    Vector3 backGroundPosition;

    // Start is called before the first frame update
    void Start()
    {
        backGroundPosition = new Vector3(2.9f, transform.position.y,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        backGroundPosition.x = gameCamera.transform.position.x*0.8f+2.9f;
        transform.position = backGroundPosition;
    }
}
