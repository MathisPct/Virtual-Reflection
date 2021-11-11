using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.SelectionManager
{
    public class Controlable : Selectable, IControlable
    {
        public event ControlHandler OnControl;
        public event DiscontrolHandler OnDiscontrol;
    }
}
