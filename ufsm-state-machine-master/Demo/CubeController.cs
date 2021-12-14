/// <summary>
/// 简单演示Demo
/// 2020年9月24日 By:熊峻玉
/// </summary>
using UnityEngine;
namespace UFSM.Demo
{
    public class CubeController : FSMBehaviour<CubeController>
    {
        //创建状态
        private RotateState rotateState = new RotateState();
        private MoveState moveState = new MoveState();
        protected void Start()
        {
            fsm.AddState(rotateState).AddTranstion("ToMove", moveState.GUID);
            fsm.AddState(moveState).AddTranstion("ToRotate", rotateState.GUID);

            //设置初始状态
            fsm.ChangeState(rotateState.GUID);
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fsm.SendMessage("ToMove");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                fsm.SendMessage("ToRotate");
            }
        }

        public override string ToString()
        {
            return this.gameObject.name;
        }
    }
    //旋转状态
    internal class RotateState : State<CubeController>
    {
        public override void OnUpdate(CubeController owner)
        {
            owner.transform.Rotate(Vector3.up);
            if (Input.GetKeyDown(KeyCode.X))
            {
                owner.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            }
        }
    }
    //旋转状态
    internal class MoveState : State<CubeController>
    {
        public override void OnUpdate(CubeController owner)
        {
            owner.transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}