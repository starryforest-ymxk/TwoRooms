using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.FSM
{
    /// <summary>
    /// ��������
    /// ��Ҫ���ڴ�����˵Ĳ���
    /// �Լ�һЩ��Ҫ���봫�������ķ���
    /// </summary>
    public abstract class BaseParameters : MonoBehaviour
    {
        [SerializeField]
        public int health;  //����ֵ
        public int attack;  //������
        public float idle_time; //����ʱ��
        public float patrol_speed;  //Ѳ������
        public float chase_speed;   //׷������
        public float attack_cooldown_time;    //����cd
        public float attack_distance;   //�������������������
        public float max_tough; //������
        public float current_tough; //��ǰ����
        public float tough_reduce;  //������
        public float memory;    //�����ǵļ���ʱ��
        public float last_see;  //���һ�η�������ʱ��
        public bool see_player; //�Ƿ�������
        public bool isDead; //�Ƿ�����        
        public bool isDizzy;    //�Ƿ�ѣ��
        public float sight_distance;    //��Ұ����
        public float sight_half_angel;  //��Ұ�Ƕ�        
        public float getParry_dizzy_time;   //������ѣ��ʱ��
        public float hit_force; //�������ȣ����ٶ�ģ�⣩
        public bool onEdge; //�Ƿ��ڵ��α�Ե
        public float ground_check_half_angle;   //��Ե���İ��
        public float ground_check_length;   //��Ե�������߳���

        [HideInInspector] public Animator animator;
        [HideInInspector] public Rigidbody2D rb2d;
        [HideInInspector] public AnimatorStateInfo anm_info;
        public LayerMask ground_mask;   //����mask
        public RaycastHit2D ground_check_left;  //������½ǵ���
        public RaycastHit2D ground_check_right; //������½ǵ���

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