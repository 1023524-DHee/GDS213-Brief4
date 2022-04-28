using UnityEngine;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager I;

    public delegate void TouchBeganEvent(Touch touch);
    public event TouchBeganEvent OnTouchBegan;

    public delegate void TouchEndedEvent(Touch touch);
    public event TouchEndedEvent OnTouchEnded;

    public bool CastFromCamera(Vector2 screenPosition, out RaycastHit hit, int layerMask = ~0) =>
        Physics.Raycast(Camera.current.ScreenPointToRay (screenPosition), out hit, 150, layerMask);

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
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
