using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace KalinKonta
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        private PlayerInputActions controls;

        private Vector2 ScrollValue => controls.Player.Rotation.ReadValue<Vector2>();

        private bool wasClicked;
        public bool WasClicked => wasClicked;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                controls = new PlayerInputActions();

                controls.Player.Click.performed += ctx => wasClicked = true;

                if (Application.isMobilePlatform) // Mobile
                {
                    EnhancedTouchSupport.Enable();
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public Vector3 GetRotationInput()
        {
#if UNITY_EDITOR
            return new Vector3(0, 0, ScrollValue.y);
#elif UNITY_WEBGL
                if (Application.isMobilePlatform)
                {
                    if (Touch.activeTouches.Count >= 2)
                    {
                        float secondaryTouchDeltaX = Touch.activeTouches[1].delta.x;
                        return new Vector3(0, 0, -secondaryTouchDeltaX);
                    }
                    return Vector3.zero;
                }
                else
                {
                    return new Vector3(0, 0, ScrollValue.y);
                }
#else
                return new Vector3(0, 0, ScrollValue.y);
#endif
        }

        private void LateUpdate()
        {
            wasClicked = false;
        }

        private void OnEnable()
        {
            controls?.Enable();
        }

        private void OnDisable()
        {
            controls?.Disable();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        } 
    }
}