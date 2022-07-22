using System;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int multiplier;
        [SerializeField] private int speed;
        [SerializeField] private bool canMove = true;
        [SerializeField] private Vector3 direction = Vector3.forward;
        [SerializeField] private float horizontalBorderSize = 5;
        private Animator playerAnimator;
        private static readonly int AnimatorParameterWalk = Animator.StringToHash("Walk");
        private static readonly int AnimatorParameterDance = Animator.StringToHash("Dance");
        
        public int Multiplier { get => multiplier; set => multiplier = value; }

        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            InputManager.Instance.onMove += OnFingerMove;
            InputManager.Instance.onStationary += OnFingerUp;
            InputManager.Instance.onUp += OnFingerUp;
            EventManager.Instance.onAllowPlayerMovement += OnAllowPlayerMovement;
        }

        private void OnDisable()
        {
            InputManager.Instance.onMove -= OnFingerMove;
            InputManager.Instance.onStationary -= OnFingerUp;
            InputManager.Instance.onUp -= OnFingerUp;
            EventManager.Instance.onAllowPlayerMovement -= OnAllowPlayerMovement;
        }

        private void OnAllowPlayerMovement()
        {
            canMove = true;
        }

        private void OnFingerMove(Vector2 delta)
        {
            if (!canMove) return;
            var value = Math.Clamp(delta.x, -1f, 1f);
            direction.x = 20 * value;
        }
        
        private void OnFingerUp()
        {
            if (!canMove) return;
            direction.x = 0;
        }

        private void Update()
        {
            Walk();
        }

        private void Walk()
        {
            if (!canMove) return;
            
            // Borders
            switch (direction.x)
            {
                case > 0 when transform.position.x > (horizontalBorderSize/2 - 0.1f):
                case < 0 when transform.position.x < -(horizontalBorderSize/2 - 0.1f):
                    direction.x = 0;
                    break;
            }

            playerAnimator.SetBool(AnimatorParameterWalk, true);
            transform.Translate(direction * (speed * Time.deltaTime), Space.World);
        }
        
        private void Stop()
        {
            playerAnimator.SetBool(AnimatorParameterWalk, false);
            Debug.Log("Stop");
        }

        private void Rise()
        {
            Debug.Log("Rise");
        }

        private void Fall()
        {
            Debug.Log("Fall");
        }

        private void CollectDiamond()
        {
            Debug.Log("CollectDiamond");
        }

        private void Die()
        {
            Debug.Log("Die");
        }

        private void Dance()
        {
            playerAnimator.SetTrigger(AnimatorParameterDance);
            Debug.Log("Dance");
        }

    }
}