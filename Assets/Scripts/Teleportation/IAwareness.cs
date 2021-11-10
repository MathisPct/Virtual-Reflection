using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.TeleportationManager
{
    public interface IAwareness
    {
        void BehaviourWhenPlayerEnter();

        void BehaviourWhenPlayerExit();
    }
}
