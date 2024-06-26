using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public float bufUpdateTime;
    public TimeGetAdapter timeGetAdapter;
    [SerializeField] Player player;
    
    private static GameManager instance;



    #region singleton
    // 인스턴스에 접근할 프로퍼티
    public static GameManager Instance
    {
        get
        {
            Debug.Log("call instance");
            // 인스턴스가 아직 존재하지 않는다면 찾거나 생성합니다.
            if (instance == null)
            {
                Debug.Log("NOOO");
                lock (new Object())
                {
                    if(instance == null)
                    {
                        instance = FindObjectOfType<GameManager>();

                        // 인스턴스가 아직도 null이라면 새로운 게임 오브젝트를 생성하여 할당합니다.
                        if (instance == null)
                        {
                            GameObject singletonObject = new GameObject();
                            instance = singletonObject.AddComponent<GameManager>();
                            singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
                            if (instance.player != null)
                            {
                                instance.timeGetAdapter = singletonObject.AddComponent<TimeManager>();
                                Debug.Log(instance.timeGetAdapter.getRemainTime());
                            }
                            else
                            {
                                Player _player = GameObject.FindObjectOfType<Player>();
                                if (_player != null)
                                {
                                    instance.player = _player;
                                    instance.timeGetAdapter = instance.GetComponent<TimeManager>();
                                }
                            }
                        }
                    }
                    instance.timeGetAdapter = instance.gameObject.GetComponent<TimeManager>();
                }
            }

            return instance;
        }
    }

    // 싱글톤 인스턴스가 존재하는지 확인하는 속성
    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    // Unity의 Awake 메서드에서 싱글톤 인스턴스를 설정합니다.
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as GameManager;
            instance.timeGetAdapter = instance.gameObject.GetComponent<TimeManager>();
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    

    // 싱글톤 인스턴스를 사용할 수 없을 때 예외를 발생시킵니다.
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    #endregion
}
