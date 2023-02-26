using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.FSM
{
    /// <summary>
    /// 参数基类
    /// 主要用于储存敌人的参数
    /// 以及一些需要传入传出参数的方法
    /// </summary>
    public abstract class BaseParameters : MonoBehaviour
    {
        [SerializeField]
        public int health;  //生命值
        public int attack;  //攻击力
        public float idle_time; //待机时间
        public float patrol_speed;  //巡逻移速
        public float chase_speed;   //追踪移速
        public float attack_cooldown_time;    //攻击cd
        public float attack_distance;   //攻击动作启用所需距离
        public float max_tough; //满韧性
        public float current_tough; //当前韧性
        public float tough_reduce;  //削韧性
        public float memory;    //对主角的记忆时间
        public float last_see;  //最后一次发现主角时间
        public bool see_player; //是否发现主角
        public bool isDead; //是否死亡        
        public bool isDizzy;    //是否眩晕
        public float sight_distance;    //视野距离
        public float sight_half_angel;  //视野角度        
        public float getParry_dizzy_time;   //被弹反眩晕时间
        public float hit_force; //击退力度（用速度模拟）
        public bool onEdge; //是否在地形边缘
        public float ground_check_half_angle;   //边缘检测的半角
        public float ground_check_length;   //边缘检测的射线长度

        [HideInInspector] public Animator animator;
        [HideInInspector] public Rigidbody2D rb2d;
        [HideInInspector] public AnimatorStateInfo anm_info;
        public LayerMask ground_mask;   //地面mask
        public RaycastHit2D ground_check_left;  //检测左下角地形
        public RaycastHit2D ground_check_right; //检测右下角地形

        public GameObject target;
        public Collider2D[] hitBoxs;
        public Transform[] patrol_points;
        public Transform[] chase_points;
        public virtual void getHit(int Damege, float tough_reduce)
        {
            health -= Damege;
            current_tough -= tough_reduce;
        }
    }
}