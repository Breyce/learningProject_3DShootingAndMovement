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
    
    public void AnimSetTrigger(string name)
    {
        anim.SetTrigger(name);
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

    public void AnimPlay(string name, int soundEffectIndex)
    {
        anim.Play(name, 0, 0);
        AudioManager.instance.PlayReloadSoundEffect(soundEffectIndex);
    }
    
    public void Fire(string name, float fadeTime)
    {
        anim.CrossFadeInFixedTime(name, fadeTime);
    }

    public void AnimSetBool(string name, bool state)
    {
        anim.SetBool(name, state);
    }

    public bool CheckAnimatorStateInfo(string name)
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName(name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
