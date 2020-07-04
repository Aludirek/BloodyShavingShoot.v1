using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwitch : MonoBehaviour
{
   public void SwitchControls()
    {
        if (PlayerControler.Instance == null)
            return;
        if (PlayerControler.Instance.movementInputType == PlayerControler.MovementInputType.ButtonBased)
            PlayerControler.Instance.movementInputType = PlayerControler.MovementInputType.PointerBased;
        else if (PlayerControler.Instance.movementInputType == PlayerControler.MovementInputType.PointerBased)
            PlayerControler.Instance.movementInputType = PlayerControler.MovementInputType.TiltInput;
        else
            PlayerControler.Instance.movementInputType = PlayerControler.MovementInputType.ButtonBased;
    }
}
