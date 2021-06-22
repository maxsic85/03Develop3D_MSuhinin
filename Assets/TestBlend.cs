using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class TestBlend : MonoBehaviour
{
    public float _forvard;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private CapsuleCollider m_Capsule;
    private AudioSource _audioSource;
    public Transform target;
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }
    public ThirdPersonCharacter character { get; private set; }
    RaycastHit hitInfo;
    Vector3 m_GroundNormal;
    public AudioClip[] m_FootstepSounds;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }
    // Используем это в следующем скрипте StateMachineBehaviour
    public void StateMethod()
    {
        print("It Works!");
    }
    public void TestEvent()
    {
        PlayFootStepAudio();
    }
    private void PlayFootStepAudio()
    {
        int n = Random.Range(1, m_FootstepSounds.Length);
        _audioSource.clip = m_FootstepSounds[n];
        _audioSource.PlayOneShot(_audioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = _audioSource.clip;
    }
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo))
        {
            m_GroundNormal = hitInfo.normal;

            m_Animator.applyRootMotion = true;
        }
        else
        {

            m_GroundNormal = Vector3.up;
            m_Animator.applyRootMotion = false;
        }
        Vector3 move = agent.desiredVelocity;
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);

        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        _forvard = move.z;
        m_Animator.SetFloat("forvard", _forvard, 0.1f, Time.deltaTime);

        if (target != null)
            agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero, false, false);
    }


}
