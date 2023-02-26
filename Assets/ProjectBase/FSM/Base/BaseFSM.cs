using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.FSM
{
    /// <summary>
    /// 状态机基类
    /// 状态转换的实现
    /// 以及一些在大多状态下生效、不需要和其他物体传递参数的方法
    /// </summary>
    /// <typeparam name="stateList">状态枚举</typeparam>
    /// <typeparam name="parameters">参数</typeparam>
    public abstract class BaseFSM<stateList, parameters> : MonoBehaviour where parameters : BaseParameters
    {
        [HideInInspector] public parameters parameter;
        protected Dictionary<stateList, IState> StatesDic = new Dictionary<stateList, IState>();
        [HideInInspector] public GameObject player;
        public IState currentState;
        private Vector3 scale_right;
        private Vector3 scale_left;       
        protected virtual void Awake()
        {
            scale_right = transform.localScale;
            scale_left = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            player = GameObject.Find("Player");
            parameter = this.GetComponent<parameters>();
            parameter.animator = this.GetComponent<Animator>();
            parameter.rb2d = this.GetComponent<Rigidbody2D>();
        }
        protected virtual void Update()
        {
            if (!(parameter.isDead || parameter.isDizzy))
            {
                currentState.OnUpdate();
                FlipTo();
                sight();
            }
            GroundCheck();
            parameter.anm_info = parameter.animator.GetCurrentAnimatorStateInfo(0);
            //movetowards不会改变刚体的速度                        
        }
        /// <summary>
        /// 状态的切换
        /// </summary>
        /// <param name="nextState">下一个状态在字典中的key</param>
        public void TransitionState(stateList nextState)
        {
            if (currentState != null)
            {
                currentState.OnExit();
            }
            currentState = StatesDic[nextState];
            currentState.OnEnter();
        }
        #region 所有敌人通用的，且大多数状态通用的方法
        /// <summary>
        /// 面朝目标
        /// </summary>
        protected void FlipTo()
        {
            if (parameter.target != null)
            {
                if (parameter.target.transform.position.x > this.transform.position.x) this.transform.localScale = scale_right;
                else if (parameter.target.transform.position.x < this.transform.position.x) this.transform.localScale = scale_left;
            }
        }
        /// <summary>
        /// 敌人的视野判定和发现玩家的行为
        /// </summary>
        protected abstract void sight();
        /// <summary>
        /// 地形判断以及行为
        /// </summary>
        protected virtual void GroundCheck()
        {
            parameter.ground_check_left = Physics2D.Raycast(this.transform.position, Vector2.down * Mathf.Cos(parameter.ground_check_half_angle) + Vector2.left * Mathf.Sin(parameter.ground_check_half_angle), parameter.ground_check_length, parameter.ground_mask);
            parameter.ground_check_right = Physics2D.Raycast(this.transform.position, Vector2.down * Mathf.Cos(parameter.ground_check_half_angle) + Vector2.right * Mathf.Sin(parameter.ground_check_half_angle), parameter.ground_check_length, parameter.ground_mask);
            parameter.onEdge = (parameter.ground_check_left && parameter.ground_check_right) ? false : true;   //如果不是两边都能检测到地面，判断为在边缘
        }
        /// <summary>
        /// 敌人被弹反
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator getParry();
        //造成伤害的三个方法        
        protected abstract void anm_show_hitbox(int i);
        protected abstract void anm_hide_hitbox(int i);
        protected abstract void OnTriggerEnter2D(Collider2D other);
        protected abstract void anm_back_to_idle();
        protected abstract void anm_SetTrigger(string name);
        protected abstract void OnDrawGizmos();
        #endregion

    }
}
