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
        [SerializeField] private GameObject actualObjectPlayerTeleportIn;

        /// <summary>
        /// Last object which received player 
        /// </summary>
        [SerializeField] private GameObject lastObjectPlayerTeleportIn;

        [SerializeField]private List<GameObject> teleportableObjects;

        [SerializeField] private List<TeleportAwareness> teleportHistory;

        void Awake()
        {
            teleportHistory = new List<TeleportAwareness>();
            //ADD SPAWN TO HISTORY
            var selection = FindObjectOfType<Spawn>();
            if(selection != null)
            {
                teleportHistory.Add(selection);
            }           
        }

        public GameObject ActualObjectPlayerTeleportIn { 
            get => actualObjectPlayerTeleportIn;
            set
            {
                lastObjectPlayerTeleportIn = actualObjectPlayerTeleportIn;
                actualObjectPlayerTeleportIn = value;
            }
        }

        /// <summary>
        /// Gère le comportement d'un objet lorsque le joueur se téléporte dessus
        /// </summary>
        public void TeleportationBehaviour()
        {
            if (lastObjectPlayerTeleportIn!=null && IsGameObjectAwareness(lastObjectPlayerTeleportIn))
            {
                lastObjectPlayerTeleportIn?.GetComponent<IAwareness>().BehaviourWhenPlayerExit();
            }
            if (IsGameObjectAwareness(actualObjectPlayerTeleportIn))
            {
                actualObjectPlayerTeleportIn.GetComponent<IAwareness>().BehaviourWhenPlayerEnter();
            }
        }

        private bool IsGameObjectAwareness(GameObject gameObject)
        {
            return teleportableObjects.Contains(gameObject);
        }

        internal void AddToHistory(TeleportAwareness teleportAwareness)
        {
            if(teleportAwareness != null)
            {
                teleportHistory.Add(teleportAwareness);
            }
        }

    }
}
