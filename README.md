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

## Development Day 3: 2023.11.11

​	个人习惯上还是将音频控制器但拎出来。不然打包在角色的代码里面的话，角色代码就会非常的庞杂，而且不好找，复用效果也不是很好。

1. 射击一般选用射线检测的方法，发出射线检测物体，判断是否打到了。即使用Physics.Raycast。
2. audioSource.isPlaying：可以用于判断当前音效是否正在播放。
3. 在整个场景中一般只有一个Audio Listener，加载在Camera上，如果设置多个的话Unity会不知道播放哪个监听器发出的声音。

## Development Day 4: 2023.11.12

1. 粒子效果：一般**火焰爆炸**，**水流**等等都用到粒子效果。需要创建诸如**火、烟或液体之类的动态对象**时，由于很难用网格 (3D) 或精灵 (2D) 描绘这种对象，因此粒子系统非常有用。网格和精灵更适合于描绘诸如房屋或汽车之类的实体对象。

2. 使用`ParticleEffect.Play()`;可以播放粒子特效。

3. **点光源**：“A Point Light is located at a point in space and sends light out in all directions equally. The direction of light hitting a surface is the line from the point of contact back to the center of the light object. The intensity diminishes with distance from the light, reaching zero at a specified range. Light intensity is inversely proportional to the square of the distance from the source. This is known as ‘inverse square law’ and is similar to how light behaves in the real world.”（摘自Unity手册）一般用于模拟场景中火花或者爆炸照亮周围环境。

4. `Quaternion.FromToRotation(fromDirection，toDirection)`：创建一个从 `fromDirection` 旋转到 `toDirection` 的旋转。

   Quaternion（摘自Unity手册）： 

   ​	四元数用于表示旋转。

   ​	它们结构紧凑，不受万向锁影响，可以轻松插值。 Unity 内部使用四元数来表示所有旋转。

   ​	它们基于复数，不容易理解。 您几乎不会有机会访问或修改单个四元数分量（x、y、z、w）； 大多数情况下，您只需要获取现有旋转（例如，来自 Transform），然后使用它们构造新的旋转 （例如，在两个旋转之间平滑插值）。 您绝大多数时间使用的四元数函数为：`Quaternion.LookRotation`、`Quaternion.Angle`、`Quaternion.Euler`、`Quaternion.Slerp`、`Quaternion.FromToRotation` 和 `Quaternion.identity`。（其他函数仅用于一些十分奇特的用例。）

   ​	您可以使用 `Quaternion.operator` 对旋转进行旋转，或对向量进行旋转。

   ​	注意，Unity 使用的是标准化的四元数。

5. `transform.position`和`transform.localPosition`：`localPosition`是相对于父物体变换的位置，`position`是世界空间中的变换位置。

6. `Vector3.Lerp`：线性插值，能起到一个平滑移动的效果。

7. `Anim.Play()`：可以直接播放指定动画。
