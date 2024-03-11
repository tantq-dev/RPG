using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private float waypointGizmoRadius = 0.3f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaitpoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaitpoint(i), GetWaitpoint(j));

            }
        }

        private int GetNextIndex(int i)
        {
            return (i + 1) % transform.childCount;
        }

        public Vector3 GetWaitpoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}

