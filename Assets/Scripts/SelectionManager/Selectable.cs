using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.SelectionManager
{
    public class Selectable : MonoBehaviour, ISelectable
    {
        public virtual TeleportationAnchor teleportationAnchor {
            get => teleportationAnchor;
            set
            {
                this.teleportationAnchor = value;
            }
        }

        public void OnDeselect()
        {
            throw new NotImplementedException();
        }

        public void OnSelect()
        {
            throw new NotImplementedException();
        }
    }
}
