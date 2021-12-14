using UFSM;
/// <summary>
/// 基于状态机的MonoBehaviour父类
/// 2020年9月24日 By:熊峻玉
/// </summary>
using UnityEngine;
public abstract class FSMBehaviour<T> : MonoBehaviour where T : FSMBehaviour<T>
{
    public FSM<T> fsm;
    protected virtual void Awake()
    {
        fsm = new FSM<T>(this as T);
    }
    protected virtual void Update()
    {
        fsm.Update();
        //  print("状态UPdate");
    }

    // protected virtual void Update()
    // {
    //     print(123);
    // }
}