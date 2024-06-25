using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{

    [SerializeField] Obstacle[] obstacle;

    private float bufUpdateTime;

    [SerializeField]
    BufManager bufManager = new BufManager();

    

    // Start is called before the first frame update
    void Start()
    {
        bufUpdateTime = GameManager.Instance.bufUpdateTime; 
        for(int i = 0; i < obstacle.Length; i++)
        {
            DamageInfo[] damages = obstacle[i].GetDamageInfo();
            for(int j = 0; j < damages.Length; j++) GetDamageInfo(damages[j]);

        }

        StartCoroutine(bufManager.BufManagerCoroutine(bufUpdateTime)); //버프매니저 코루틴 실행
        

    }

    void GetDamageInfo(DamageInfo damageInfo)
    {
        bufManager.Add(damageInfo.bufList);
    }
}
