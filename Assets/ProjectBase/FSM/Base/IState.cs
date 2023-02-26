using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.FSM
{
    public interface IState
    {
        public void OnEnter();
        public void OnUpdate();
        public void OnExit();
    }
}
