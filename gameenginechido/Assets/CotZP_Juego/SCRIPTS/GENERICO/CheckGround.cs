using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public Transform rayOrigin;
    public LayerMask groundLayers;
    public float detectionDistance;
    public bool rayDraw;

    public bool IsTouchingGround()
    {
        return Physics.Raycast(rayOrigin.position, -rayOrigin.up, detectionDistance, groundLayers);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (rayDraw && rayOrigin != null)
        {
            Gizmos.DrawRay(rayOrigin.position, -rayOrigin.up * detectionDistance);
        }
    }

}
