using UnityEngine;
using System.Collections;

public class TemporaryCameraScript : MonoBehaviour
{
    // TODO: Temp code, should be split up later
    // Note - The camera later will be ECS
    public GameObject PlayerObject;

    [Header("Settings")]
    public Vector3 CameraOffset;
    public float MinCameraRadius;
    public float MaxCameraRadius;
    public float CameraSmoothing;
    public Vector3 CursorWorldSpacePosition;
    private Vector3 CameraPoint;
    private Vector3 PlayerPosition;
    private Plane CursorPlane = new Plane(Vector3.down, 0);
    private Camera CameraComponent;

    void Start()
    {
        CameraComponent = GetComponent<Camera>();
    }

    void Update()
    {
        if (PlayerObject == null)
        {
            return;
        }

        // Get Cursor position in world space
        Vector3 ScreenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(ScreenPosition);
        if (CursorPlane.Raycast(ray, out float dist))
        {
            CursorWorldSpacePosition = ray.GetPoint(dist);
        }

        PlayerPosition = PlayerObject.transform.position;
        PlayerPosition.y = 0;

        Vector3 diff = (CursorWorldSpacePosition - PlayerPosition) / 2;
        Vector3 dir = Vector3.Normalize(diff);
        float len = Mathf.Min(MaxCameraRadius, diff.magnitude);

        // Deadzone
        // if (len > MinCameraRadius) {
        //     CharacterLookAtCursor();
        // }

        CameraPoint = PlayerPosition + dir * len;
        transform.position = CameraPoint + CameraOffset;
    }

    void OnDrawGizmosSelected()
    {
        if (PlayerObject == null)
        {
            return;
        }

        // Draws a blue line from this transform to the target
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(PlayerPosition, CursorWorldSpacePosition);
        Gizmos.DrawSphere(CameraPoint, 0.5f);
    }
}
