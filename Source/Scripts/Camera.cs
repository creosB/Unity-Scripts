using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Creos.Cameras
{
    public class Camera : MonoBehaviour 
    {
        public Transform m_Target;

    	// Use this for initialization
    	void Start () 
        {
            HandleCamera();	
    	}
    	
    	// Update is called once per frame
    	void Update () 
        {
            HandleCamera();	
    	}
        protected virtual void HandleCamera()
        {
            if(!m_Target)
            {
                return;
            }
        }
    }
}
