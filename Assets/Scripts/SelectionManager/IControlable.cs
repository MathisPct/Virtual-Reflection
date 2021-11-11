using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.SelectionManager
{
    public delegate void ControlHandler();
    public delegate void DiscontrolHandler();
    public interface IControlable
    {
        event ControlHandler OnControl;
        event DiscontrolHandler OnDiscontrol;
    }
}
