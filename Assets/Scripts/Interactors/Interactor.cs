using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;


namespace Assets.Scripts.Interactors
{
    public abstract class Interactor : XRRayInteractor
    {
        /// <summary>
        /// Permet de savoir si l'interactor est entrain de séléctionner un objet
        /// </summary>
        /// <returns>True si l'interractor séléctionne actuellement un objet</returns>
        public abstract bool IsInteractorSelectInteractable();
    }
}

