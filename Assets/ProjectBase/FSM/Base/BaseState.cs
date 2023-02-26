using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.FSM
{
    public abstract class BaseState<FSM, Parameters>
    {
        protected FSM manager;
        protected Parameters parameter;
    }
}