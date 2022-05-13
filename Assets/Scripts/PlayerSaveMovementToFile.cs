using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveMovementToFile : MonoBehaviour
{
    private SaveMovement _save;
    private ReplayAfterTime _replay;

    private void Awake()
    {
        _save = GetComponent<SaveMovement>();
        _replay = GameObject.Find("Timer").GetComponent<ReplayAfterTime>();
    }

    private void OnEnable()
    {
        _replay._restartGame.AddListener(SaveToFile);

    }

    private void OnDisable()
    {

        _replay._restartGame.RemoveListener(SaveToFile);
    }

    private void SaveToFile()
    {
        SaveFile.saveMovementsJump.Add(_save.Jump);
        SaveFile.saveMovementsRotate.Add(_save.Rotation);
    }
}
