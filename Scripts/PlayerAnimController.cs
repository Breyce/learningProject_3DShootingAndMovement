using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public static PlayerAnimController instance;

    private Animator anim;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        //组件获取
        anim = GetComponent<Animator>();
    }
    
    public void InspectWeapon()
    {
        anim.SetTrigger("Inspect");
    }

    public void PlayerMovement(int moveState)
    {
        if (moveState == 0)//0为静止，1为走，2为跑
        {
            anim.SetBool("isRun",false);
            anim.SetBool("isWalk", false);
        }else if (moveState == 1)
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isWalk", true);
        }else if(moveState == 2)
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isWalk", false);
        }
    }

    public void ReloadAmmo(int changeState)
    {
        if (changeState == 0) //0为打空后换弹，1为没打空换弹
        {
            anim.Play("reloadOutOfAmmo", 0, 0);
            AudioManager.instance.PlayReloadSoundEffect(0);
        }
        else if(changeState == 1)
        {
            anim.Play("reloadAmmoLeft", 0, 0);
            AudioManager.instance.PlayReloadSoundEffect(1);
        }
    }
}
