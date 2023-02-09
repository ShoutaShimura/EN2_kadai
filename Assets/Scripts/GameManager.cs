using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject clearText;

    public void SceneReset()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
    }

    public void ChangeScene(string nextScene)  // 新しくメソッド追加
    {
        SceneManager.LoadScene(nextScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        //もしゴールオブジェクトのコライダーに接触した時の処理。
        if (other.gameObject.tag == "Player")
        {
            //ゲームクリアテキストを表示させてキャラクターを非表示にします。
            clearText.SetActive(true);

        }
    }

}
