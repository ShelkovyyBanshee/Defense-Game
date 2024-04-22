

using UnityEngine;

namespace DefenseGame.Testing
{
    public class TurnPauseOnByButtonComponent : MonoBehaviour
    {
        [SerializeField] KeyCode _pauseKey = KeyCode.P; 
        private IPauseController _pauseComponent;
        private bool _isPauseOn;

        private void Awake()
        {
            _pauseComponent = GetComponent<PauseController>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(_pauseKey))
            {
                _isPauseOn = !_isPauseOn;
                if (_isPauseOn) _pauseComponent.AddPauseRequest();
                else _pauseComponent.RemovePauseRequest();
            }
        }
    }
}