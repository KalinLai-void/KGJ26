using UnityEngine;
using UnityEngine.InputSystem;

namespace KalinKonta
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        private PlayerInputActions controls;

        private Vector3 GyroValue => controls.Player.GyroRotation.ReadValue<Vector3>();

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
                    if (UnityEngine.InputSystem.Gyroscope.current != null)
                    {
                        InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
                    }
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
                    return GyroValue; 
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