using UnityEngine;

public class FlashlightBehavior : MonoBehaviour
{
    void Update()
    {
        // Get mouse position in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Keep it on the same 2D plane

        // Get direction from spotlight to mouse
        Vector3 direction = (mousePos - transform.position).normalized;

        // Calculate rotation angle (adjusted for 2D orientation)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation (assuming the spotlight's "forward" is along the right (X-axis))
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
}
