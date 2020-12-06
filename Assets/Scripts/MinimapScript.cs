using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{

    public Transform DroneParent;

    private void LateUpdate()
    {
        Vector3 newPosition = DroneParent.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, DroneParent.eulerAngles.y, 0f);
    }

}
