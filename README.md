# 3DShooting

## Development Day 1: 2023.11.9

1. Unity 在场景中如何操作镜头：右键按住可以操控镜头，同时用WASD来移动，Q垂直下移，E垂直上移。
2. 获取组件的两种方式：
   1. 直接在Unity当中拖拽赋值：坏处是在打包时有可能丢失。
   2. 在代码中赋值：使用 GetComponent<>() 函数对组件进行获取。
3. transform.forward/right：forward是蓝色轴的方向，right是红色轴的方向。
4. transform.normalized：规范化，返回与原向量相比方向不变长度为1的向量。
5. Input.GetAxis在将轴映射到鼠标时该值会有所不同，不会再在[-1,1]的范围内。此时，该值为当前鼠标增量乘以轴灵敏度。正值表示鼠标向右/下移动，负值表示向上/左移动。

## Development Day 2: 2023.11.10

1. Cursor.lockState：**确定硬件指针是否锁定到视图的中心，受限于窗口或者根本不受限制**。锁定时，光标位于视图中心且无法移动，光标在此状态下不可见。 值为：CursorLockMode.Locked时，光标锁定在游戏窗口的中心.

   ```
   Cursor.lockState = CursorLockMode.Locked
   ```

2. Tooltip：设置的变量名称会在鼠标悬停时展示提示信息。

3. SerializeField：强制序列化，让private属性的值可以在Inspector当中显示。

4. Physics.CheckSphere()：如果有任何碰撞体与设定的球体有叠加就会返回true，用于判断球体是否右碰撞。

5. Physics.Raycast：向场景中的所有碰撞体投射射线，时刻碰撞。该射线起点为 /origin/，朝向 /direction/，长度为 /maxDistance/。

   ```c#
   Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.4f)
   
   //可以通过slopeHit.normal得到其法线方向。RaycastHit.normal：射线打中的表面的法线。
   ```

   其中： out 关键字可以将射线内容保存到指定的RaycastHit结构体当中。
