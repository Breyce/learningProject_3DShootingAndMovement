using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    //摇摆参数
    public float amount;
    public float smoothAmount;//摇摆平滑值
    public float maxAmount;//最大摇摆幅度

    [SerializeField]private Vector3 orginPosition;

    // Start is called before the first frame update
    void Start()
    {
        orginPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //获取鼠标轴值
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        //限制
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        //手臂位置变化
        Vector3 finalPosition = new Vector3(movementX, movementY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition,finalPosition + orginPosition, Time.deltaTime*smoothAmount);
     }
}
