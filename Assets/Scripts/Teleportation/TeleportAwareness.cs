using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.XRExtension
{
    /// <summary>
    /// Allow player to teleport in transform destination
    /// </summary>
    public class TeleportAwareness : BaseTeleportationInteractable
    {

        [SerializeField] private Transition screenFade;

        [SerializeField] private TeleportationManager teleportationManager;

        /// <summary>
        /// Transform of destination
        /// </summary>
        private Transform transformOfDestination;

        private void Awake()
        {
            base.Awake();
            screenFade = FindObjectOfType<Transition>();
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            if (teleportTrigger == TeleportTrigger.OnSelectExited)
                StartCoroutine(FadeSequence(base.OnSelectExited, args));
        }

        public Transform TransformOfDestination { get => transformOfDestination; set => transformOfDestination = value; }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void OnValidate()
        {
            if (transformOfDestination == null)
                transformOfDestination = transform;
        }

        /// <summary>
        /// Called when gizmos are drawn.
        /// </summary>
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            GizmoHelpers.DrawWireCubeOriented(transformOfDestination.position, transformOfDestination.rotation, 1f);

            GizmoHelpers.DrawAxisArrows(transformOfDestination, 1f);
        }


        protected override bool GenerateTeleportRequest(XRBaseInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
        {
            if (transformOfDestination == null)
                return false;
            teleportRequest.destinationPosition = transformOfDestination.position;
            teleportRequest.destinationRotation = transformOfDestination.rotation;
            teleportationManager.AddToHistory(GetTeleportAwarnessFromRay(raycastHit));
            teleportationManager.TeleportationBehaviour();
            return true;
        }

        private TeleportAwareness GetTeleportAwarnessFromRay(RaycastHit raycastHit)
        {
            var teleportAware = raycastHit.collider.gameObject.GetComponentInParent<TeleportAwareness>();
            return teleportAware;
        }

        /// <summary>
        /// Disable interation and fade to black (transition).
        /// Wait, then do the teleport stuff, fade from black to transparent transition, enable interaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private IEnumerator FadeSequence<T>(UnityAction<T> action, T args)
            where T : BaseInteractionEventArgs
        {
            //disable interation and fade to black transition
            interactionManager.enabled = false;
            screenFade.FadeIn();
            // Wait, then do the teleport stuff, fade from black to transparent transition, enable interaction
            yield return new WaitForSeconds(screenFade.DurationFadeIn);
            action.Invoke(args);
            screenFade.FadeOut();
            interactionManager.enabled = true;
        }
    }
}
