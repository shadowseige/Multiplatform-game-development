using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform drone;
    private void LateUpdate()
    {
        Vector3 newPosition = drone.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, drone.eulerAngles.y, 0f);
    }
}
