using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager> {
    
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    [SerializeField] private SwipeDetection swipeDetection;
    private TouchControls touchControls;
    private Camera mainCamera;

    private void Awake() {
        touchControls = new TouchControls();
        mainCamera = Camera.main;
    }
    
    private void OnEnable() {
        touchControls.Enable();
    }

    private void OnDisable() {
        touchControls.Disable();
    }

    private void Start() {
        touchControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        touchControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context) {
        if (OnStartTouch != null) OnStartTouch(CameraUtil.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }
    
    private void EndTouchPrimary(InputAction.CallbackContext context) {
        if (OnEndTouch != null) OnEndTouch(CameraUtil.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 PrimaryPosition() {
        return CameraUtil.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

    public Direction SwipeDirection() {
        return swipeDetection.direction;
    }
}
