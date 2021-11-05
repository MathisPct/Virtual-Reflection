using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.SelectionManager
{
    public interface IControlable
    {
        void OnControl(SelectEnterEventArgs args);
        void OnDiscontrol();
    }
}
