using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.SelectionManager
{
    public interface ISelectable
    {
        TeleportationAnchor teleportationAnchor
        {
            get;
            set;
        }
        void OnSelect();
        void OnDeselect();
    }
}
