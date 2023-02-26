using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.FSM
{
    /// <summary>
    /// ״̬������
    /// ״̬ת����ʵ��
    /// �Լ�һЩ�ڴ��״̬����Ч������Ҫ���������崫�ݲ����ķ���
    /// </summary>
    /// <typeparam name="stateList">״̬ö��</typeparam>
    /// <typeparam name="parameters">����</typeparam>
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
            //movetowards����ı������ٶ�                        
        }
        /// <summary>
        /// ״̬���л�
        /// </summary>
        /// <param name="nextState">��һ��״̬���ֵ��е�key</param>
        public void TransitionState(stateList nextState)
        {
            if (currentState != null)
            {
                currentState.OnExit();
            }
            currentState = StatesDic[nextState];
            currentState.OnEnter();
        }
        #region ���е���ͨ�õģ��Ҵ����״̬ͨ�õķ���
        /// <summary>
        /// �泯Ŀ��
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
        /// ���˵���Ұ�ж��ͷ�����ҵ���Ϊ
        /// </summary>
        protected abstract void sight();
        /// <summary>
        /// �����ж��Լ���Ϊ
        /// </summary>
        protected virtual void GroundCheck()
        {
            parameter.ground_check_left = Physics2D.Raycast(this.transform.position, Vector2.down * Mathf.Cos(parameter.ground_check_half_angle) + Vector2.left * Mathf.Sin(parameter.ground_check_half_angle), parameter.ground_check_length, parameter.ground_mask);
            parameter.ground_check_right = Physics2D.Raycast(this.transform.position, Vector2.down * Mathf.Cos(parameter.ground_check_half_angle) + Vector2.right * Mathf.Sin(parameter.ground_check_half_angle), parameter.ground_check_length, parameter.ground_mask);
            parameter.onEdge = (parameter.ground_check_left && parameter.ground_check_right) ? false : true;   //����������߶��ܼ�⵽���棬�ж�Ϊ�ڱ�Ե
        }
        /// <summary>
        /// ���˱�����
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator getParry();
        //����˺�����������        
        protected abstract void anm_show_hitbox(int i);
        protected abstract void anm_hide_hitbox(int i);
        protected abstract void OnTriggerEnter2D(Collider2D other);
        protected abstract void anm_back_to_idle();
        protected abstract void anm_SetTrigger(string name);
        protected abstract void OnDrawGizmos();
        #endregion

    }
}
