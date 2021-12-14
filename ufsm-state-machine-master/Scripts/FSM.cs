/// <summary>
/// 状态机,管理所有状态
/// 2020年9月24日 By:熊峻玉
/// </summary>
using System.Collections.Generic;
using UnityEngine;
namespace UFSM
{
    public class FSM<T>
    {
        public bool debugMode = false;
        /// <summary>
        /// 当前状态
        /// </summary>
        public State<T> currentState { get; private set; }
        public T owner { get; private set; }

        public FSM(T owner, State<T> firstState = null)
        {
            this.owner = owner;
            if (firstState != null)
            {
                currentState = firstState;
                return;
            }
            currentState = new State<T>();
        }

        // 所有状态
        private List<State<T>> states = new List<State<T>>();

        //添加并返回状态
        public State<T> AddState(State<T> state)
        {
            state.fsm = this;
            states.Add(state);
            return state;
        }

        public State<T> AddState<T1>() where T1 : State<T>, new()
        {
            T1 state = new T1();
            state.fsm = this;
            states.Add(state);
            return state;
        }
        //根据id获取状态
        public State<T> GetState(string id)
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].GUID == id)
                {
                    return states[i];
                }
            }
            return null;
        }
        /// <summary>
        /// 发送消息,以改变状态
        /// </summary>
        /// <param name="condition"></param>
        /// <param name=""></param>
        public void SendMessage(string condition)
        {
            for (int i = 0; i < states.Count; i++)
            {
                var transtionMap = states[i].transtionMap;
                if (transtionMap.ContainsKey(condition))
                {
                    string nextStateId = transtionMap[condition];
                    if (nextStateId != currentState.GUID)
                    {
                        ChangeState(nextStateId);
                    }
                    return;
                }
            }
            throw new System.Exception("没有找到对应的转换条件:" + condition);
        }
        /// <summary>
        /// 直接改变状态
        /// </summary>
        public void ChangeState(string nextStateUid)
        {
            State<T> nextState = states.Find(s => s.GUID == nextStateUid);
            ChangeState(nextState);
        }

        /// <summary>
        /// 直接改变状态
        /// </summary>
        public void ChangeState(State<T> nextState)
        {
            if (debugMode) Debug.Log(string.Format("{0}:\t{1}\t{2}", owner, currentState.GUID, "ChangeState"));

            currentState.OnExit(owner);//当前状态退出
            if (debugMode) Debug.Log(string.Format("{0}:\t{1}\t{2}", owner, currentState.GUID, "Exit"));

            nextState.OnEnter(owner);//下一个状态进入
            if (debugMode) Debug.Log(string.Format("{0}:\t{1}\t{2}", owner, nextState.GUID, "Enter"));
            currentState = nextState;
        }

        public void Update()
        {
            currentState.OnUpdate(owner);
        }
    }
}