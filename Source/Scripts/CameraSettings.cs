using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Creos.Cameras
{
    public class CameraSettings : Camera
    {
        public float m_Height = 4.0f;
        public float m_Distance = 15.0f;

        [SerializeField]
        private float m_Angle = -90.0f;

        [SerializeField]
        private float m_SmoothSpeed = 0.5f;

        private Vector3 refVelocity;

        protected override void HandleCamera()
        {
            base.HandleCamera();

            //Build World position vector
            Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
            // Debug.DrawLine(m_Target.position, worldPosition, Color.red);

            //Build our Rotated vector
            Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;
            // Debug.DrawLine(m_Target.position, rotatedVector, Color.green);

            //Move our position
            Vector3 flatTargetPosition = m_Target.position;
            flatTargetPosition.y = 0f;
            Vector3 finalPosition = flatTargetPosition + rotatedVector;
            // Debug.DrawLine(m_Target.position, finalPosition, Color.blue);

            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, m_SmoothSpeed);
            transform.LookAt(flatTargetPosition);

        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
            if (m_Target)
            {
                Gizmos.DrawLine(transform.position, m_Target.position);
                Gizmos.DrawSphere(m_Target.position, 1.5f);
            }
            Gizmos.DrawSphere(transform.position, 1.5f);
        }
    }
}
