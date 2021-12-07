using Assets.Scripts.TeleportationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.XRExtension
{
    public class TeleportationManager : MonoBehaviour
    {
        /// <summary>
        /// Object where the player in teleport 
        /// Is modify when player teleport in object 
        /// </summary>
        [SerializeField] private TeleportAwareness actualObjectPlayerTeleportIn;

        /// <summary>
        /// Last object which received player 
        /// </summary>
        [SerializeField] private TeleportAwareness lastObjectPlayerTeleportIn;


        [SerializeField]private List<GameObject> teleportableObjects;

        [SerializeField] private List<TeleportAwareness> history;

        [SerializeField] private TeleportationProvider teleportationProvider;

        [SerializeField] private bool teleport = false;

        void Awake()
        {
            history = new List<TeleportAwareness>();
            //ADD SPAWN TO HISTORY
            var selection = FindObjectOfType<Spawn>();
            if(selection != null)
            {
                history.Add(selection);
            }           
        }

        public TeleportAwareness ActualObjectPlayerTeleportIn {
            get
            {
                TeleportAwareness actual = null;
                actual = history[history.Count - 1];
                actualObjectPlayerTeleportIn = actual;
                return actualObjectPlayerTeleportIn;
            }
        }

        public TeleportAwareness LastObjectPlayerTeleportIn
        {
            get
            {
                TeleportAwareness previous = null;
                previous = history[history.Count - 2];
                lastObjectPlayerTeleportIn = previous;
                return lastObjectPlayerTeleportIn;
            }
        }

        /// <summary>
        /// Gère le comportement d'un objet lorsque le joueur se téléporte dessus
        /// </summary>
        public void TeleportationBehaviour()
        {         
            if (LastObjectPlayerTeleportIn?.GetComponent<IAwareness>() != null)
                LastObjectPlayerTeleportIn.GetComponent<IAwareness>().BehaviourWhenPlayerExit();
            if(ActualObjectPlayerTeleportIn?.GetComponent<IAwareness>() != null)
                ActualObjectPlayerTeleportIn.GetComponent<IAwareness>().BehaviourWhenPlayerEnter();
        }

        public void AddToHistory(TeleportAwareness teleportAwareness)
        {
            if(teleportAwareness != null)
            {
                history.Add(teleportAwareness);
            }
        }

        public void GoToPreviousPoint()
        {
            if(history.Count - 1 > 0)
            {
                //behaviour player exit of the previous point of teleportation
                IAwareness oldActual = history[history.Count - 1].GetComponent<IAwareness>();
                //delete actual element where player is
                history.RemoveAt(history.Count - 1);
                if (oldActual != null) oldActual.BehaviourWhenPlayerExit();
                //behaviour player enter of the previous point of teleportation
                IAwareness newActual = ActualObjectPlayerTeleportIn.GetComponent<IAwareness>();
                if(newActual != null) newActual.BehaviourWhenPlayerEnter();
                GenerateRequest();
            }
        }

        private void GenerateRequest()
        {
            TeleportRequest teleportRequest = new TeleportRequest();
            teleportRequest.destinationPosition = ActualObjectPlayerTeleportIn.gameObject.transform.position;
            teleportRequest.destinationRotation = ActualObjectPlayerTeleportIn.gameObject.transform.rotation;
            teleportRequest.matchOrientation = MatchOrientation.TargetUpAndForward;
            this.teleportationProvider.QueueTeleportRequest(teleportRequest);
        }

        private void Update()
        {
            if (teleport)
            {
                GoToPreviousPoint();
                teleport = false;
            }
        }
    }
}
