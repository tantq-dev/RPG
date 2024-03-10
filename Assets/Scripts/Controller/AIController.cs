using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] private float chaseDistance = 5f;
        private GameObject player;
        private Fighter fighter;
        private Health health;
        private Mover mover;

        private Vector3 guardPosition;
        void Awake()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = this.transform.position;

        }
        void Update()
        {
            if (health.IsDead()) return;
            if (InRange() && fighter.CanAttack(player))
            {
                Attack();

            }
            else
            {

                mover.StartMoveAction(guardPosition);

            }



        }

        private bool InRange()
        {
            return Vector3.Distance(this.transform.position, player.transform.position) < chaseDistance;
        }

        private void Attack()
        {
            fighter.Attack(player);
        }




        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, chaseDistance);
        }

    }

}
