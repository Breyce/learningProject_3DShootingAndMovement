using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    //ҡ�ڲ���
    public float amount;
    public float smoothAmount;//ҡ��ƽ��ֵ
    public float maxAmount;//���ҡ�ڷ���

    [SerializeField]private Vector3 orginPosition;

    // Start is called before the first frame update
    void Start()
    {
        orginPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //��ȡ�����ֵ
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        //����
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        //�ֱ�λ�ñ仯
        Vector3 finalPosition = new Vector3(movementX, movementY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition,finalPosition + orginPosition, Time.deltaTime*smoothAmount);
     }
}
