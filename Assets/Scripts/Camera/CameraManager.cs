using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;

    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position - new Vector3(0, -4f, 7f);
        cameraOffset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion cameraTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 2, Vector3.up);
        cameraOffset = cameraTurnAngle * cameraOffset;
        Vector3 newPosition = player.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, 0.5f);

        transform.LookAt(player);
    }
}
