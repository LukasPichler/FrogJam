using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ReplayAfterTime : MonoBehaviour
{
    [SerializeField]
    private float _timeUntilReplay=10f;

    [SerializeField]
    private Transform _fire;

    [SerializeField]
    private Transform _moveFireFrom;

    [SerializeField]
    private Transform _moveFireTo;

    [SerializeField]
    private SpriteRenderer _sprite;
    
    private Material _desolver;

    [SerializeField]
    private float _maxDesolv;

    [SerializeField]
    private float _minDesolv;
    

    private int _numberOfDucks = 4;

    private float _clock = 0f;

    public UnityEvent _restartGame;


    private int _frogsInGoal = 0;

    private void Awake()
    {
        PlayerInputAction _playerInputAction = new PlayerInputAction();
        _desolver = _sprite.material;
        _playerInputAction.Player.Enable();
        _playerInputAction.Player.Restart.started += ReloadSceneDeleteSaves;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_restartGame == null)
        {
            _restartGame = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {

        _clock += Time.deltaTime;
        float desolvValue = Mathf.Lerp(_maxDesolv,_minDesolv, _clock / _timeUntilReplay);
        
        _desolver.SetFloat("_Fade",desolvValue);

        _fire.position = Vector3.Lerp(_moveFireFrom.position, _moveFireTo.position, _clock / _timeUntilReplay);



    }

    private void FixedUpdate()
    {
        int currentTime = Mathf.Max(0, (int)(_timeUntilReplay - _clock));
        if(currentTime == 0)
        {
            ReloadSceneNoSave();
        }
        
    }


    public void ReloadSceneSaved()
    {
        if (_restartGame != null)
        {
            _restartGame.Invoke();
        }
        SaveFile.currentPlayer++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadSceneNoSave()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FrogInGoal()
    {
        _frogsInGoal++;

        if(_frogsInGoal >= _numberOfDucks)
        {
            Debug.Log("Win");
        }
        if(_frogsInGoal > SaveFile.currentPlayer)
        {
            ReloadSceneSaved();
        }
    }

    public void ReloadSceneDeleteSaves(InputAction.CallbackContext context)
    {
        SaveFile.DeleteInput();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
