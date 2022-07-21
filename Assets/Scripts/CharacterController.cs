using System;
using UnityEngine;

namespace Game
{
    public class CharacterController : MonoBehaviour
    {
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
    }
}