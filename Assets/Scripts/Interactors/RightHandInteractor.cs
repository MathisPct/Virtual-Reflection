using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.Interactors
{
    public class RightHandInteractor : Interactor
    {
        private Interactor leftHand;

        private void Awake()
        {
            base.Awake();
            leftHand = FindObjectOfType<LeftHandInteractor>();
        }

        /// <summary>
        /// L'interractor peut séléctionner l'objet à condition que l'autre interactor ne séléctionne 
        /// pas déjà un objet
        /// </summary>
        /// <param name="interactable"></param>
        /// <returns></returns>
        public override bool CanSelect(XRBaseInteractable interactable)
        {
            return !leftHand.IsInteractorSelectInteractable() && base.CanSelect(interactable);
        }

        public override bool IsInteractorSelectInteractable()
        {
            return selectTarget != null;
        }
    }
}
