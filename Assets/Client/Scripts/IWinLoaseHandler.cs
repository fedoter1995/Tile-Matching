using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWinLoaseHandler
{
    event Action<bool> WinLoseEvent;
}
