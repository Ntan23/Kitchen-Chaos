using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    public delegate void HasProgressCounterEvent(float progressValue);
    public event HasProgressCounterEvent OnProgressChanged;
}
