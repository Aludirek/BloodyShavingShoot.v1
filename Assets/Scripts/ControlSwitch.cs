using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwitch : MonoBehaviour
{
   public void SwitchControls()
    {
        if (PlayerControler.Instance == null)
            return;
        if (PlayerControler.Instance.movementInputType == GameMenu.PlayerMovementInputType.ButtonBased)
            PlayerControler.Instance.movementInputType = GameMenu.PlayerMovementInputType.PointerBased;
        else if (PlayerControler.Instance.movementInputType == GameMenu.PlayerMovementInputType.PointerBased)
            PlayerControler.Instance.movementInputType = GameMenu.PlayerMovementInputType.TiltInput;
        else
            PlayerControler.Instance.movementInputType = GameMenu.PlayerMovementInputType.ButtonBased;
    }
}
