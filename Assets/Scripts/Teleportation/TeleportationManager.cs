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
        /// Comportement de la téléportation. Active lorsque le joueur arrive sur 
        /// un élément sur lequel il peut se téléporter
        /// </summary>
        public void TeleportationBehaviour()
        {
            foreach(var teleportableObject in teleportableObjects)
            {
                var teleportable = teleportableObject.GetComponent<IAwareness>();
                if (teleportable == null) continue;
                if(teleportable.GetGameObject() == actualObjectPlayerTeleportIn)
                {
                    lastObjectPlayerTeleportIn = actualObjectPlayerTeleportIn;
                    teleportable.TeleportationBehaviour();
                }
                if(actualObjectPlayerTeleportIn != lastObjectPlayerTeleportIn)
                {
                    teleportable.NotTeleportationBehaviour();
                }
            }
        }

        public void Update()
        {
            TeleportationBehaviour();
            Debug.Log("Actual object" + actualObjectPlayerTeleportIn);
            Debug.Log("Last object" + lastObjectPlayerTeleportIn);
        }
    }
}
