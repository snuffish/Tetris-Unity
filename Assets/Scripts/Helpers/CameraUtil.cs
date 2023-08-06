using UnityEngine;

public class CameraUtil : MonoBehaviour {
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position) {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
}