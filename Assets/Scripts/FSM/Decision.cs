using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(BaseStateMachine state);
}
