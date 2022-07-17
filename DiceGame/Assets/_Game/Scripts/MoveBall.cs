using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public GameObject ball;
    public float ballMoveSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ball.transform.position += Vector3.right * ballMoveSpeed * Time.deltaTime;
    }
}
