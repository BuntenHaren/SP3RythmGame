using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask layerMask;


    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
            {
            var raycastX = raycastHit.point.x;
            var raycastZ = raycastHit.point.z;
            transform.position = new Vector3 (raycastX, transform.position.y, raycastZ);
        }

    }
}
