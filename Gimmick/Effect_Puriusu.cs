using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Puriusu : MonoBehaviour
{
    public Player playerScript;
    public Animations anime;
    GameMode gmode;

    // Start is called before the first frame update
    void Start()
    {
        gmode = GameMode.InGame;
    }

    private void Update()
    {

    }
    void OnParticleCollision(GameObject obj)
    {
        Debug.Log("��������:" + obj.gameObject.tag + ":" + obj.gameObject.name);
        
        //Debug.Log(inputV);
        //if (obj.gameObject.tag == "tabako")
        //{
        Debug.Log(anime.Getanime);
            if(obj.gameObject.tag == "Player" && !anime.GetUp && gmode == GameMode.InGame && !GameManager.InPoos)
            {
                Debug.Log("<color=white>��������</color>");
                GameManager.IsDamage = true;
                gmode = GameMode.GameOver;
            }

        //}

    }
}
