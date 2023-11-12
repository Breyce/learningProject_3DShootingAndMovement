using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public Transform shooterPoint; //���λ��

    [Header("Bullet Info")]
    public int bulletsMag = 31; //�������ӵ�����
    public int range = 100; //���������
    public int bulletLeft = 60; //����
    public int currentBullets; //��ǰ�ӵ�����

    [Header("Shooting Info")]
    public float fireRate = 0.1f; //����
    private float fireTime = 0; //��ʱ��
    private bool gunShoot;
    private RaycastHit hit;

    [Header("Practical System")]
    public ParticleSystem muzzleFlash;// ǹ�ڻ���
    public Light muzzleFlashLight;//ǹ�ڵƹ� 
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
        //������
        reloadBulletKey = KeyCode.R;
        inspectKey = KeyCode.F;

        //������ʼ��
        currentBullets = bulletsMag;

        //��ʼ����������
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
        //ʵ�ֻ�������
        if (Input.GetKey(reloadBulletKey) && currentBullets < bulletsMag)
        {
            ReloadBulletKey();
        }
        //ʵ�ֲ鿴����
        if(Input.GetKeyDown(inspectKey))
        {
            PlayerAnimController.instance.InspectWeapon();
        }
        //ͨ����ʱ����������
        if (fireTime < fireRate)
        {
            fireTime += Time.deltaTime;
        }
    }

    /*
     * �������
     */
    public void GunFire()
    {
        if (fireTime < fireRate || currentBullets <= 0) return;
        else
        {
            Vector3 shootDirection = shooterPoint.forward;
            if (Physics.Raycast(shooterPoint.position, shootDirection, out hit, range))
            {
                Debug.Log(hit.transform.name + "����");
                GameObject hitParticleEffect = Instantiate(hitParticle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));//ʵ�����ӵ����еĻ����Ч��
                GameObject bulletHoleEffect = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));//ʵ�����ӵ����еĵ�����Ч��

                Destroy(hitParticleEffect, 1);
                Destroy(bulletHoleEffect, 3);
            }
            currentBullets--;

            //���������Ч�ͻ�������
            AudioManager.instance.PlayShootSound();
            muzzleFlashLight.enabled = true;
            muzzleFlash.Play();

            //�����ӵ�
            UpdateAmmoUI();

            //���ü�ʱ����
            fireTime = 0;
        }
    }

    /*
     * ��������
     */
    public void ReloadBulletKey()
    {
        if (bulletLeft <= 0) return;

        //���㱸��������
        int bulletChange = bulletsMag - currentBullets;

        if(bulletLeft > bulletChange)
        {
            //���±�������
            bulletLeft -= bulletChange;
            if(currentBullets == 0)
            {
                PlayerAnimController.instance.ReloadAmmo(0);
            }
            else
            {
                PlayerAnimController.instance.ReloadAmmo(1);
            }
            //��䵱ǰ�ӵ�
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

            //���µ�ǰ�ӵ�����
            currentBullets += bulletLeft;

            //���±�������
            bulletLeft = 0;
        }


        UpdateAmmoUI();
    }

    /*
     * ����UI
     */
    public void UpdateAmmoUI()
    {
        ammoText.text = "BULLET LEFT: " + currentBullets + "/" + bulletLeft;
    }
}
