using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelBoss : MonoBehaviour
{
   [SerializeField]
   private GameObject boss;

    private void Update()
    {
        if (boss == null)
        {
            AudioManager.Instance.Win();
            MenuManager.Instance.Victory.SetActive(true);
        }
    }
}
