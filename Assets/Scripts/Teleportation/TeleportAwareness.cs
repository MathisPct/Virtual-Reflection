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
        /// Transform of destination game object where the script is attached
        /// </summary>
        private Transform transformOfDestination;

        private void Awake()
        {
            base.Awake();
            screenFade = FindObjectOfType<Transition>();
            teleportationManager = FindObjectOfType<TeleportationManager>();
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
            TeleportAwareness destination = GetTeleportableFromRay(raycastHit);
            teleportationManager.AddToHistory(destination);
            teleportationManager.TeleportWithRequest(ref teleportRequest);
            return false;
        }

        private TeleportAwareness GetTeleportableFromRay(RaycastHit raycastHit)
        {
            var teleportAware = raycastHit.collider.gameObject.GetComponentInParent<TeleportAwareness>();
            return teleportAware;
        }

    }
}
