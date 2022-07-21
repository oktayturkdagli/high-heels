using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int multiplier;
        
        public int Multiplier { get => multiplier; set => multiplier = value; }

        private void OnEnable()
        {
            EventManager.Instance.onStartLevel += OnStartLevel;
        }

        private void OnDisable()
        {
            EventManager.Instance.onStartLevel -= OnStartLevel;
        }
        
        private void OnStartLevel()
        {
            //Do Nothing
        }

        protected void Walk()
        {
            Debug.Log("Walk");
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
            Debug.Log("Dance");
        }

    }
}