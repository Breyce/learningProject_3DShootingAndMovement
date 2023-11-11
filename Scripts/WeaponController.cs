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

    [Header("KeyBinds")]
    private KeyCode reloadBulletKey;

    [Header("UI Config")]
    public Image shootPoint;
    public Text ammoText;

    void Start()
    {
        reloadBulletKey = KeyCode.R;
        currentBullets = bulletsMag;
        UpdateAmmoUI();
    }

    void Update()
    {
        gunShoot = Input.GetMouseButton(0);
        if (gunShoot)
        {
            GunFire();
        }
        //实现换弹操作
        if (Input.GetKey(reloadBulletKey) && currentBullets < bulletsMag)
        {
            ReloadBulletKey();
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
            }
            currentBullets--;

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

            //填充当前子弹
            currentBullets = bulletsMag;
        }
        else if(bulletLeft < bulletChange)
        {
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
