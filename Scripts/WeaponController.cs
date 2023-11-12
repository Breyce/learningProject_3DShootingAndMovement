using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public Transform shooterPoint; //射击位置

    [Header("Bullet Info")]
    public int bulletsMag = 31; //弹夹中子弹数量
    public int range = 100; //武器的射程
    public int bulletLeft = 60; //备弹
    public int currentBullets; //当前子弹数量

    [Header("Shooting Info")]
    public float fireRate = 0.1f; //射速
    private float fireTime = 0; //计时器
    private bool gunShoot;
    private RaycastHit hit;

    [Header("Practical System")]
    public ParticleSystem muzzleFlash;// 枪口火焰
    public Light muzzleFlashLight;//枪口灯光 
    public GameObject hitParticle;
     public GameObject bulletHole;

    [Header("KeyBinds")]
    private KeyCode reloadBulletKey;
    private KeyCode inspectKey;

    [Header("UI Config")]
    public Image shootPoint;
    public Text ammoText;

    void Start()
    {
        //按键绑定
        reloadBulletKey = KeyCode.R;
        inspectKey = KeyCode.F;

        //参数初始化
        currentBullets = bulletsMag;

        //初始化函数调用
        UpdateAmmoUI();


    }

    void Update()
    {
        gunShoot = Input.GetMouseButton(0);
        if (gunShoot)
        {
            GunFire();
        }
        else
        {
            muzzleFlashLight.enabled = false;
        }
        //实现换弹操作
        if (Input.GetKey(reloadBulletKey) && currentBullets < bulletsMag)
        {
            ReloadBulletKey();
        }
        //实现查看操作
        if(Input.GetKeyDown(inspectKey))
        {
            PlayerAnimController.instance.InspectWeapon();
        }
        //通过计时器控制射速
        if (fireTime < fireRate)
        {
            fireTime += Time.deltaTime;
        }
    }

    /*
     * 射击函数
     */
    public void GunFire()
    {
        if (fireTime < fireRate || currentBullets <= 0) return;
        else
        {
            Vector3 shootDirection = shooterPoint.forward;
            if (Physics.Raycast(shooterPoint.position, shootDirection, out hit, range))
            {
                Debug.Log(hit.transform.name + "打到了");
                GameObject hitParticleEffect = Instantiate(hitParticle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));//实例出子弹击中的火光特效。
                GameObject bulletHoleEffect = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));//实例出子弹击中的弹坑特效。

                Destroy(hitParticleEffect, 1);
                Destroy(bulletHoleEffect, 3);
            }
            currentBullets--;

            //播放射击音效和火光和粒子
            AudioManager.instance.PlayShootSound();
            muzzleFlashLight.enabled = true;
            muzzleFlash.Play();

            //更新子弹
            UpdateAmmoUI();

            //重置计时器。
            fireTime = 0;
        }
    }

    /*
     * 换弹函数
     */
    public void ReloadBulletKey()
    {
        if (bulletLeft <= 0) return;

        //计算备弹减少量
        int bulletChange = bulletsMag - currentBullets;

        if(bulletLeft > bulletChange)
        {
            //更新备弹数量
            bulletLeft -= bulletChange;
            if(currentBullets == 0)
            {
                PlayerAnimController.instance.ReloadAmmo(0);
            }
            else
            {
                PlayerAnimController.instance.ReloadAmmo(1);
            }
            //填充当前子弹
            currentBullets = bulletsMag;
        }
        else if(bulletLeft < bulletChange)
        {
            if (currentBullets == 0)
            {
                PlayerAnimController.instance.ReloadAmmo(0);
            }
            else
            {
                PlayerAnimController.instance.ReloadAmmo(1);
            }

            //更新当前子弹数量
            currentBullets += bulletLeft;

            //更新备弹数量
            bulletLeft = 0;
        }


        UpdateAmmoUI();
    }

    /*
     * 更新UI
     */
    public void UpdateAmmoUI()
    {
        ammoText.text = "BULLET LEFT: " + currentBullets + "/" + bulletLeft;
    }
}
