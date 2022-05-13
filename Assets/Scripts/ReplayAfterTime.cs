using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayAfterTime : MonoBehaviour
{
    [SerializeField]
    private float _timeUntilReplay=10f;

    private float _clock = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        _clock += Time.deltaTime;

    }
}
