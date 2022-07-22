using System;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int multiplier;
        [SerializeField] private int speed;
        [SerializeField] private bool canMove = true;
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
            InputManager.Instance.onSwipe += OnSwipe;
            EventManager.Instance.onStartLevel += OnStartLevel;
        }

        private void OnDisable()
        {
            InputManager.Instance.onSwipe -= OnSwipe;
            EventManager.Instance.onStartLevel -= OnStartLevel;
        }
        
        private void OnSwipe(SwipeType swipeType)
        {
            Debug.Log(swipeType.ToString());
        }
        
        private void OnStartLevel()
        {
            //Do Nothing
        }

        protected void Walk(Vector3 movementVector)
        {
            if (canMove)
            {
                playerAnimator.SetBool(AnimatorParameterWalk, true);
                transform.Translate(movementVector.normalized * speed * Time.deltaTime, Space.World);
                Debug.Log("Walk" + movementVector.normalized);
            }
        }
        
        protected void Stop()
        {
            playerAnimator.SetBool(AnimatorParameterWalk, false);
            Debug.Log("Stop");
        }

        protected void Rise()
        {
            Debug.Log("Rise");
        }

        protected void Fall()
        {
            Debug.Log("Fall");
        }

        protected void CollectDiamond()
        {
            Debug.Log("CollectDiamond");
        }

        protected void Die()
        {
            Debug.Log("Die");
        }

        protected void Dance()
        {
            playerAnimator.SetTrigger(AnimatorParameterDance);
            Debug.Log("Dance");
        }

    }
}