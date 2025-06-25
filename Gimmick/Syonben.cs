using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syonben : MonoBehaviour
{
    //[SerializeField] GameObject play;
    [SerializeField] Player player;
    GameMode gmode;
    private void Start()
    {
        //player = play.GetComponent<Player>();
        gmode = GameMode.InGame;
    }
    void OnParticleCollision(GameObject obj)
    {
        
        if (obj.gameObject.tag == "tabako")
        {
            
            if((!player.Getbool || (player.Getshift_bool && player.transform.lossyScale.x <= 0.0f)) && gmode == GameMode.InGame && !GameManager.InPoos)
            {
                Debug.Log("‚ ‚½‚Á‚½");
                GameManager.IsDamage = true;
                gmode = GameMode.GameOver;
            }
            //if (gmode == GameMode.InGame && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            //{
            //    Debug.Log("‚ ‚½‚Á‚½");
            //    GameManager.IsDamage = true;
            //    gmode = GameMode.GameOver;
                
            //}
           
        }

    }
}
