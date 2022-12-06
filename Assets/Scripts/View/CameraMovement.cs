using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float offsetY = 1.5f;
    public float offsetZ = -6f;

    [Range(1f, 0f)]
    public float smoothness = 0.1f;
	GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

    void LateUpdate() {
        Vector3 targetPos = new Vector3(player.transform.position.x, 
                                        player.transform.position.y, 
                                        player.transform.position.z + offsetZ);
        targetPos.y += offsetY;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothness);
    }
}
