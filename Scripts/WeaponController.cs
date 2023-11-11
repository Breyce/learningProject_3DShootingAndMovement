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
        //ʵ�ֻ�������
        if (Input.GetKey(reloadBulletKey) && currentBullets < bulletsMag)
        {
            ReloadBulletKey();
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
            }
            currentBullets--;

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

            //��䵱ǰ�ӵ�
            currentBullets = bulletsMag;
        }
        else if(bulletLeft < bulletChange)
        {
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
