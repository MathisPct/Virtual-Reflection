using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.Interactors
{
    public class LeftHandInteractor : Interactor
    {
        private Interactor rightHand;

        private void Awake()
        {
            base.Awake();
            rightHand = FindObjectOfType<RightHandInteractor>();
        }
            
        /// <summary>
        /// L'interractor peut séléctionner l'objet à condition que l'autre interactor ne séléctionne 
        /// pas déjà un objet
        /// </summary>
        /// <param name="interactable"></param>
        /// <returns></returns>
        public override bool CanSelect(XRBaseInteractable interactable)
        {
            return !rightHand.IsInteractorSelectInteractable() && base.CanSelect(interactable);
        }

        public override bool IsInteractorSelectInteractable()
        {
            return this.selectTarget != null;
        }
    }
}
