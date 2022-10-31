using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BaseState : ScriptableObject
{
    public virtual void Execute(BaseStateMachine machine) { }
}
