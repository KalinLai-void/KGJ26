using UnityEngine;

namespace KalinKonta
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        private PlayerInputActions controls;

        public Vector2 ScrollValue => controls.Player.Rotation.ReadValue<Vector2>();

        private bool wasClicked;
        public bool WasClicked => wasClicked;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                controls = new PlayerInputActions();

                controls.Player.Click.performed += ctx => wasClicked = true;
            }
            else
            {
                Destroy(gameObject);
            }
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
    }
}