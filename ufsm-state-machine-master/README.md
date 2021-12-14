# UFSM状态机

#### 介绍
适合于Unity中使用的状态机,方便为一个MonoBehaviour子类添加状态控制,代码精简,使用简单
# 简单使用教程
## 1.控制类继承自FSMBehaviour
```csharp
public class CubeController : FSMBehaviour<CubeController>
```

## 2.定义状态类

```csharp
//旋转状态
class RotateState : State<CubeController>
{
    public override void OnUpdate(CubeController owner)
    {
        ower.transform.Rotate(Vector3.up*180* Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.X))
        {
            ower.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }
        //也可以重写OnEnter和OnExit方法添加逻辑
    }
}
//旋转状态
class MoveState : State<CubeController>
{
    public override void OnUpdate(CubeController owner)
    {
        ower.transform. Translate(Vector3.forward * Time.deltaTime);
    }
}
```
## 3.创建状态对象
```csharp
RotateState rotateState = new RotateState();
MoveState moveState = new MoveState();
```
## 4.添加状态并设置状态切换条件,再设置一个初始状态就可以跑起来了
```csharp
//下面两行代码添加状态并设置该状态拥有的转换条件
fsm.AddState(rotateState).AddTranstion("ToMove", moveState.GUID);
fsm.AddState(moveState).AddTranstion("ToRotate", rotateState.GUID);
fsm.ChangeState(rotateState.GUID); //设置初始状态
```
## 状态转换

```csharp
fsm.SendMessage("ToMove");
```

## DebugMode

```csharp
fsm.debugMode = true;
```


## 简单演示Demo

 ./Demo/Demo.unity

![](README.assets/Video_2020-09-29_180242.gif)





