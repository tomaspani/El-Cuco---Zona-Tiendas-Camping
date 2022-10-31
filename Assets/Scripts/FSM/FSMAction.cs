using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class FSMAction : ScriptableObject
{
    public abstract void Execute(BaseStateMachine stateMachine);
}