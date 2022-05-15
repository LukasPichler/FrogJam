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
    private bool _inTutorial=false;

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
    
    [SerializeField]
    private int _numberOfDucks = 4;

    private float _clock = 0f;

    public UnityEvent _restartGame;

    public UnityEvent _wonGame;

    private int _frogsInGoal = 0;

    private PlayerInputAction _playerInputAction;
    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _desolver = _sprite.material;
        _playerInputAction.Player.Enable();
        _playerInputAction.Player.Restart.started += ReloadSceneDeleteSaves;
    }

    private void OnDisable()
    {
        _playerInputAction.Player.Restart.started -= ReloadSceneDeleteSaves;

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
        StartCoroutine(WaitBeforeReloadSaved());
    }

    private IEnumerator WaitBeforeReloadSaved()
    {
        if (_restartGame != null)
        {
            _restartGame.Invoke();
        }
        SaveFile.currentPlayer++;
        yield return new WaitForSeconds(1f);

        Loader.LoadRewind(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadSceneNoSave()
    {
        if (!_inTutorial)
        {
            CalculateScore.Death();
        }
        Loader.LoadFast(SceneManager.GetActiveScene().buildIndex);
    }

    public void FrogInGoal()
    {
        _frogsInGoal++;

        if(_frogsInGoal >= _numberOfDucks)
        {
            StartCoroutine(LoadNextLevel());
        }else
        if(_frogsInGoal > SaveFile.currentPlayer)
        {
            ReloadSceneSaved();
        }
    }

    private IEnumerator LoadNextLevel()
    {
        _playerInputAction.Player.Restart.started -= ReloadSceneDeleteSaves;
        SaveFile.DeleteInput();
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        if (_wonGame != null)
        {
            _wonGame.Invoke();
        }
        if (!_inTutorial)
        {
            CalculateScore.CalculateNewScore((int)(_timeUntilReplay - _clock));
        }
        yield return new WaitForSeconds(1f);
        Loader.Load(scene);
    }

    public void ReloadSceneDeleteSaves(InputAction.CallbackContext context)
    {
        SaveFile.DeleteInput();
        Loader.LoadFast(SceneManager.GetActiveScene().buildIndex);
    }

    public void SubscribeToWon(UnityAction call)
    {
        if (_wonGame != null)
        {
            _wonGame = new UnityEvent();
        }
        _wonGame.AddListener(call);
    }
    public void DeSubscribeToWon(UnityAction call)
    {

        if (_wonGame != null)
        {
            _wonGame = new UnityEvent();
        }
        _wonGame.RemoveListener(call);
    }
}
