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
    // �ν��Ͻ��� ������ ������Ƽ
    public static GameManager Instance
    {
        get
        {
            Debug.Log("call instance");
            // �ν��Ͻ��� ���� �������� �ʴ´ٸ� ã�ų� �����մϴ�.
            if (instance == null)
            {
                Debug.Log("NOOO");
                lock (new Object())
                {
                    if(instance == null)
                    {
                        instance = FindObjectOfType<GameManager>();

                        // �ν��Ͻ��� ������ null�̶�� ���ο� ���� ������Ʈ�� �����Ͽ� �Ҵ��մϴ�.
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

    // �̱��� �ν��Ͻ��� �����ϴ��� Ȯ���ϴ� �Ӽ�
    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    // Unity�� Awake �޼��忡�� �̱��� �ν��Ͻ��� �����մϴ�.
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

    

    // �̱��� �ν��Ͻ��� ����� �� ���� �� ���ܸ� �߻���ŵ�ϴ�.
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    #endregion
}
