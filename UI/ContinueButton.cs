using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public static event Action OnContinueGame;


    public void ContinueGame()
    {
        OnContinueGame?.Invoke();
    }
}
