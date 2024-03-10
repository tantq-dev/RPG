using System;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health health;

        void Awake()
        {
            health = this.GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {

                    continue;

                }
                if (Input.GetMouseButtonDown(0))
                {

                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {

            RaycastHit raycastHit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out raycastHit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(raycastHit.point);

                }

                return true;

            }
            return false;
        }



        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
