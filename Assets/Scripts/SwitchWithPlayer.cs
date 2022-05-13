using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWithPlayer : MonoBehaviour
{
    private PlaySavings _playSavings;

    [SerializeField]
    private GameObject playerPrefab;

    private void Awake()
    {
        _playSavings = GetComponent<PlaySavings>();
        if(_playSavings.number == SaveFile.currentPlayer)
        {
            Instantiate(playerPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }

  
}
