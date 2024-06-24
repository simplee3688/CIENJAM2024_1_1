using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float remainedTime;
    public float bufUpdateTime;


    private static GameManager instance;

    // �ν��Ͻ��� ������ ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� �������� �ʴ´ٸ� ã�ų� �����մϴ�.
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                // �ν��Ͻ��� ������ null�̶�� ���ο� ���� ������Ʈ�� �����Ͽ� �Ҵ��մϴ�.
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameManager>();
                    singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
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
            DontDestroyOnLoad(gameObject);
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
    
}
