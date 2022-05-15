using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        PlayerInputAction _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
