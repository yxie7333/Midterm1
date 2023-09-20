using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    public float followSpeed = 5f;
    public float mapMinX = -23.88f;
    public float mapMaxX = 34.7f;
    public float mapMinY = -22.75f;
    public float mapMaxY = 11.8f;
    private float camHalfWidth;
    private float camHalfHeight;


    // Start is called before the first frame update
    void Start()
    {
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        Vector3 playerPosition = player.position;
        float clampedX = Mathf.Clamp(playerPosition.x, mapMinX + camHalfWidth, mapMaxX - camHalfWidth);
        float clampedY = Mathf.Clamp(playerPosition.y, mapMinY + camHalfHeight, mapMaxY - camHalfHeight);

        Vector3 targetPosition = new Vector3(clampedX, clampedY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

}
