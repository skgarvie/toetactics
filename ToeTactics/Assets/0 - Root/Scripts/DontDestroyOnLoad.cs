using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {

        private bool _isCreated = false;
        // Use this for initialization

        public void Awake()
        {
            if (!_isCreated)
            {
                DontDestroyOnLoad(this);
                _isCreated = true;
            }
        }
	
        void Start () {	
		
        }	
    }
}
