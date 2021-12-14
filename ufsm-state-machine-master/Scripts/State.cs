/// <summary>
/// 状态父类
/// 2020年9月24日 By:熊峻玉
/// </summary>
using System;
using System.Collections.Generic;

namespace UFSM
{
    public class State<T>
    {
        public FSM<T> fsm { get; set; }
        //状态唯一id,类型字符串
        public string GUID { get; }
        public State()
        {
            GUID = Guid.NewGuid().ToString();
        }
        //状态方法
        public virtual void OnEnter(T owner) { }
        public virtual void OnUpdate(T owner)
        {
        }
        public virtual void OnExit(T owner)
        {
        }
        //转换条件
        public Dictionary<string, string> transtionMap = new Dictionary<string, string>();
        /// <summary>
        /// 添加转换条件
        /// </summary>
        /// <param name="condition">条件名称</param>
        /// <param name="stateName">下一个状态的名称</param>
        public State<T> AddTranstion(string condition, string nextStateId)
        {
            transtionMap.Add(condition, nextStateId);
            return this;
        }
    }
}