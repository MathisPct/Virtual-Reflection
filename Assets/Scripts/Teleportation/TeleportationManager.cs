using Assets.Scripts.TeleportationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.XRExtension
{
    public class TeleportationManager : MonoBehaviour
    {
        /// <summary>
        /// Object where the player in teleport 
        /// Is modify when player teleport in object 
        /// </summary>
        private GameObject actualObjectPlayerTeleportIn;

        /// <summary>
        /// Last object which received player 
        /// </summary>
        private GameObject lastObjectPlayerTeleportIn;

        [SerializeField]private List<GameObject> teleportableObjects;

        public GameObject ActualObjectPlayerTeleportIn { get => actualObjectPlayerTeleportIn; set => actualObjectPlayerTeleportIn = value; }

        /// <summary>
        /// Gère le comportement d'un objet lorsque le joueur se téléporte dessus
        /// </summary>
        public void TeleportationBehaviour()
        {
            if (IsGameObjectAwareness(actualObjectPlayerTeleportIn))
            {
                lastObjectPlayerTeleportIn = actualObjectPlayerTeleportIn;
                actualObjectPlayerTeleportIn.GetComponent<IAwareness>().TeleportationBehaviour();
            }
            if(actualObjectPlayerTeleportIn != lastObjectPlayerTeleportIn)
            {
                if(lastObjectPlayerTeleportIn != null) lastObjectPlayerTeleportIn.GetComponent<IAwareness>().NotTeleportationBehaviour();
            }
        }

        private bool IsGameObjectAwareness(GameObject gameObject)
        {
            return teleportableObjects.Contains(gameObject);
        }

        public void Update()
        {
            TeleportationBehaviour();
            Debug.Log("Actual object" + actualObjectPlayerTeleportIn);
            Debug.Log("Last object" + lastObjectPlayerTeleportIn);
        }
    }
}
