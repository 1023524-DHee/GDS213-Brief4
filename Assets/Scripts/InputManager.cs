using UnityEngine;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager I;

    [SerializeField] private LayerMask _cameraRaycastMask;
    public static LayerMask CameraRaycastMask;

    public delegate void TouchBeganEvent(Touch touch);
    public event TouchBeganEvent OnTouchBegan;

    public delegate void TouchEndedEvent(Touch touch);
    public event TouchEndedEvent OnTouchEnded;

    public bool CastFromCamera(Vector2 screenPosition, out RaycastHit hit) =>
        Physics.Raycast(Camera.current.ScreenPointToRay (screenPosition), out hit, 150, CameraRaycastMask);

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
            CameraRaycastMask = _cameraRaycastMask;
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began: OnTouchBegan?.Invoke(touch);
                        break;
                    case TouchPhase.Ended: OnTouchEnded?.Invoke(touch);
                        break;
                }
            }
        }
    }
}
