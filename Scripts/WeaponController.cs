using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public Transform shooterPoint; //���λ��

    [Header("ReloadInfo")]
    private bool isReload;

    [Header("Bullet Info")]
    public int bulletsMag = 31; //�������ӵ�����
    public int range = 100; //���������
    public int bulletLeft = 60; //����
    public int currentBullets; //��ǰ�ӵ�����

    [Header("Shooting Info")]
    public float fireRate = 0.1f; //����
    private float fireTime = 0; //��ʱ��
    private bool gunShoot;
    private bool isAming;
    private RaycastHit hit;
    public Transform casingSpawnPoint;//�ӵ��׳�λ��
    public Transform casingPrefab;//�ӵ���Ԥ����

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
    public Camera mainCamera;

    void Start()
    {
        //������
        reloadBulletKey = KeyCode.R;
        inspectKey = KeyCode.F;

        //������ʼ��
        currentBullets = bulletsMag;

        //��ʼ����������
        UpdateAmmoUI();

        //��ȡ���
        mainCamera = Camera.main;

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
        bool reloadAmmoLeft = PlayerAnimController.instance.CheckAnimatorStateInfo("reloadAmmoLeft");
        bool reloadOutOfAmmo = PlayerAnimController.instance.CheckAnimatorStateInfo("reloadOutOfAmmo");

        if (reloadOutOfAmmo || reloadAmmoLeft)
        {
            isReload = true;
        }
        else
        {
            isReload = false;
        }

        if (Input.GetKey(reloadBulletKey) && currentBullets < bulletsMag)
        {
            ReloadBulletKey();
        }
        //ʵ�ֲ鿴����
        if(Input.GetKeyDown(inspectKey))
        {
            PlayerAnimController.instance.AnimSetTrigger("Inspect");
        }
        //ͨ����ʱ����������
        if (fireTime < fireRate)
        {
            fireTime += Time.deltaTime;
        }
        //ʵ����׼����
        AmingFire();
    }

    /*
     * �������
     * GunFire()�������������
     * AmingFire()����׼������
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

            //�����������
            if (!isAming)
            {
                //������ͨ���𶯻�
                PlayerAnimController.instance.Fire("fire",0.1f);
            }
            else
            {
                //��׼״̬�²��ſ��𶯻���
                PlayerAnimController.instance.Fire("aimFire", 0.1f);
            }

            currentBullets--;

            //�����׳� 
            Instantiate(casingPrefab, casingSpawnPoint.transform.position, casingSpawnPoint.transform.rotation);
            int soundIndex = Random.Range(1, 5);
            AudioManager.instance.PlayNoNeedStopSound(soundIndex);

            //���������Ч�ͻ�������
            AudioManager.instance.PlayNoNeedStopSound(0);
            muzzleFlashLight.enabled = true;
            muzzleFlash.Play();

            //�����ӵ�
            UpdateAmmoUI();

            //���ü�ʱ����
            fireTime = 0;
        }
    }

    public void AmingFire()
    {
        if(Input.GetMouseButton(1) && !isReload && !PlayerAnimController.instance.CheckAnimatorStateInfo("run"))
        {
            if (PlayerAnimController.instance.CheckAnimatorStateInfo("aimOut")) return;
            else
            {
                //��׼��1. ׼����ʧ�� 2. ��Ұ��ǰ
                isAming = true;
                
                //��׼��������Ч
                PlayerAnimController.instance.AnimSetBool("isAim", true);

                //׼����ʧ
                shootPoint.gameObject.SetActive(false);

                //��Ұ��ǰ
                mainCamera.fieldOfView = 25;
            }
        }
        else
        {
            //����׼
            isAming = false;

            PlayerAnimController.instance.AnimSetBool("isAim", false);

            //׼�ǳ���
            shootPoint.gameObject.SetActive(true);

            //��Ұ��λ
            mainCamera.fieldOfView = 60;
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
                PlayerAnimController.instance.AnimPlay("reloadOutOfAmmo", 0);
            }
            else
            {
                PlayerAnimController.instance.AnimPlay("reloadAmmoLeft", 1);
            }
            //��䵱ǰ�ӵ�
            currentBullets = bulletsMag;
        }
        else if(bulletLeft < bulletChange)
        {
            if (currentBullets == 0)
            {
                PlayerAnimController.instance.AnimPlay("reloadOutOfAmmo", 0);
            }
            else
            {
                PlayerAnimController.instance.AnimPlay("reloadAmmoLeft", 1);
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
