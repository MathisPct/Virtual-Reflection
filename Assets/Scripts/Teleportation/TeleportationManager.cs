using Assets.Scripts.TeleportationManager;
using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private List<GameObject> teleportableObjects;

        [SerializeField] private List<TeleportAwareness> history;

        [SerializeField] private bool teleportSpawn = false;

        [SerializeField] private bool teleportPrevious = false;

        [SerializeField] private XRInteractionManager interactionManager;

        [SerializeField] private TeleportationProvider teleportationProvider;

        [SerializeField] private Transition screenFade;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private RandomSelectionSound randomTeleportationSounds;

        void Awake()
        {
            history = new List<TeleportAwareness>();
            screenFade = FindObjectOfType<Transition>();
            interactionManager = FindObjectOfType<XRInteractionManager>();
            audioSource = GetComponentInChildren<AudioSource>();
            randomTeleportationSounds = FindObjectOfType<RandomSelectionSound>();

            //FIND TELEPORTATION PROVIDER
            var tProvider = FindObjectOfType<TeleportationProvider>();
            if (tProvider != null)
            {
                this.teleportationProvider = tProvider;
            }

            //ADD SPAWN TO HISTORY
            var selection = FindObjectOfType<Spawn>();
            if (selection != null)
            {
                history.Add(selection);
            }
        }

        public TeleportAwareness ActualObjectPlayerTeleportIn
        {
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
                if(history.Count > 1) previous = history[history.Count - 2];
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
            if (ActualObjectPlayerTeleportIn?.GetComponent<IAwareness>() != null)
                ActualObjectPlayerTeleportIn.GetComponent<IAwareness>().BehaviourWhenPlayerEnter();
        }

        public void AddToHistory(TeleportAwareness teleportAwareness)
        {
            if (teleportAwareness != null) history.Add(teleportAwareness);
        }

        public void GoToPreviousPoint()
        {
            if (history.Count - 1 > 0)
            {
                
                IAwareness oldActual = history[history.Count - 1].GetComponent<IAwareness>();

                //delete actual element where player is
                history.RemoveAt(history.Count - 1);

                if (oldActual != null)
                {
                    //behaviour triggered when the player move from the old current teleportable
                    oldActual.BehaviourWhenPlayerExit();
                }

                TeleportToGameObject(ActualObjectPlayerTeleportIn.gameObject);
            }
        }

        /// <summary>
        /// Allow player to teleport back to the spawn
        /// </summary>
        public void GoToSpawn()
        {
            while (history.Count != 1)
            {
                GoToPreviousPoint();
            }
        }

        private void Update()
        {
            if (teleportSpawn)
            {
                GoToSpawn();
                teleportSpawn = false;
            }

            if (teleportPrevious)
            {
                GoToPreviousPoint();
                teleportPrevious = false;
            }
        }

        /// <summary>
        /// Allow to teleport player into game object
        /// </summary>
        /// <param name="gameObject">Game object where player is teleport</param>
        public void TeleportToGameObject(GameObject gameObject)
        {         
            TeleportRequest teleportRequest = new TeleportRequest();
            teleportRequest.destinationPosition = gameObject.transform.position;
            teleportRequest.destinationRotation = gameObject.transform.rotation;
            teleportRequest.matchOrientation = MatchOrientation.TargetUpAndForward;         
            StartCoroutine(TeleportSequence(teleportRequest));
        }

        public void TeleportWithRequest(ref TeleportRequest teleportRequest)
        {
            StartCoroutine(TeleportSequence(teleportRequest));
        }


        /// <summary>
        /// Disable interation and fade to black (transition).
        /// Wait, then do the teleport stuff, fade from black to transparent transition, enable interaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private IEnumerator TeleportSequence(TeleportRequest request)

        {
            //disable interation and fade to black transition
            audioSource.PlayOneShot(randomTeleportationSounds.RandomTeleportationAudioClip());
            interactionManager.enabled = false;
            screenFade.FadeIn();
            // Wait, then do the teleport stuff, fade from black to transparent transition, enable interaction
            yield return new WaitForSeconds(screenFade.DurationFadeIn);

            this.TeleportationBehaviour();
            //ajout aux teleportations à effectuer
            this.teleportationProvider.QueueTeleportRequest(request);
            screenFade.FadeOut();
            interactionManager.enabled = true;
        }
    }
}