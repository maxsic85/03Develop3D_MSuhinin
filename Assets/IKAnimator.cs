using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IKAnimator : MonoBehaviour
{
    /// <summary>
    /// For hand and hand
    /// </summary>
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _handLFollow;
    [SerializeField] private Transform _handRFollow;
    [SerializeField] private Transform _LookObj;
    [SerializeField] private float _handLWight;
    [SerializeField] private float _handRWight;

    [SerializeField] private Transform _handL;
    [SerializeField] private Transform _handR;
    [SerializeField] private Vector3 _rightHandPos;
    [SerializeField] private Quaternion _rightHandRot;
    [SerializeField] private Vector3 _leftHandPos;
    [SerializeField] private Quaternion _lefttHandRot;

    /// <summary>
    /// Fof foot
    /// </summary>
    [SerializeField] private float _footRightWight;
    [SerializeField] private Transform _rightFootLeg;
    [SerializeField] private Transform _rightFoot;
    [SerializeField] private int _rightHash;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Vector3 _rightFootPos;
    [SerializeField] private Quaternion _rightFootRot;
    [SerializeField] private Dictionary<GameObject, float> _currents;

    [SerializeField] private float _maxDistanceToWall = 2f;
    // Start is called before the first frame update
    void Start()
    {
        _currents = new Dictionary<GameObject, float>();
        _animator = GetComponent<Animator>();
        _rightHash = Animator.StringToHash("right_foot");
        _rightFootLeg = _animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);//колено
        _rightFoot = _animator.GetBoneTransform(HumanBodyBones.RightFoot);//колено
        _handR = _animator.GetBoneTransform(HumanBodyBones.RightHand);//рука
        _handL = _animator.GetBoneTransform(HumanBodyBones.LeftHand);//рука
    }
    private void OnAnimatorIK(int layerIndex)
    {
        FootIK();
        HandUpOnWall();
        // FollowHandTo();

        if (!_LookObj) return;
        LookAtObj(_LookObj.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        RaycastHit hit;
        if (other.tag == "follower")
        {
            if (Physics.Raycast(Camera.main.transform.position, other.transform.position, out hit))
            {
                Debug.DrawLine(Camera.main.transform.position, other.transform.position);
                //Add to Dictionary follower obj and distance
                if (!_currents.ContainsKey(other.gameObject))
                {
                    _currents.Add(other.gameObject, hit.distance);
                }
                else
                {
                    //Update distance
                    //_currents[other.gameObject] = hit.distance;
                    _currents[other.gameObject] = Vector3.Distance(Camera.main.transform.position,other.transform.position);

                    foreach (var item in _currents)
                    {                    
                        print("Upd" +item.Key +"/"+ (int)item.Value);
                    }
                }
                //Sorting
                _currents = _currents.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                //Select min distance
                var first = _currents.First();
                _LookObj = first.Key.transform;
            }
            else
            {
                _LookObj = null;
                _currents.Remove(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "follower")
        {
            _LookObj = null;
        }
    }
    private void LookAtObj(GameObject look)
    {
        _animator.SetLookAtWeight(1);
        _animator.SetLookAtPosition(look.transform.position);
        print("Iam looking on   " + look.name);
    }
    private void HandUpOnWall()
    {
        RaycastHit _hitR;
        RaycastHit _hitL;

        if (Physics.Raycast(_handR.position, Vector3.forward, out _hitR, _maxDistanceToWall, _mask))
        {
            _rightHandPos = Vector3.Lerp(_handR.position, _hitR.point + Vector3.forward * 0.1f, Time.deltaTime * 100);
            _rightHandRot = Quaternion.FromToRotation(transform.up, _hitR.normal) * transform.rotation;
            _handRWight = 1 - ((1f * _hitR.distance) / _maxDistanceToWall);
        }
        else
        {
            _rightHandPos = _handR.position;
            _rightHandRot = _handR.rotation;
            _handRWight = 0;
        }
        if (Physics.Raycast(_handL.position, Vector3.forward, out _hitL, _maxDistanceToWall, _mask))
        {
            _leftHandPos = Vector3.Lerp(_handL.position, _hitL.point + Vector3.forward * 0.01f, Time.deltaTime * 100);
            _lefttHandRot = Quaternion.FromToRotation(transform.up, _hitL.normal) * transform.rotation;
            _handLWight = 1 - ((1f * _hitL.distance) / _maxDistanceToWall);
        }
        else
        {
            _leftHandPos = _handL.position;
            _lefttHandRot = _handL.rotation;
            _handLWight = 0;
        }

        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _handRWight);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, _handRWight);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandPos);
        _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandRot);

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _handLWight);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _handLWight);
        _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandPos);
        _animator.SetIKRotation(AvatarIKGoal.LeftHand, _lefttHandRot);
    }
    private void FootIK()
    {
        _footRightWight = 1; //_animator.GetFloat(_rightHash);
        RaycastHit _hit;
        if (Physics.Raycast(_rightFootLeg.position, Vector3.down, out _hit, 1.2f, _mask))
        {
            _rightFootPos = Vector3.Lerp(_rightFoot.position, _hit.point + Vector3.up * 0.1f, Time.deltaTime * 10);
            _rightFootRot = Quaternion.FromToRotation(transform.up, _hit.normal) * transform.rotation;
        }
        else
        {
            _rightFootPos = _rightFoot.position;
            _rightFootRot = _rightFoot.rotation;
        }
        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, _footRightWight);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, _footRightWight);

        _animator.SetIKPosition(AvatarIKGoal.RightFoot, _rightFootPos);
        _animator.SetIKRotation(AvatarIKGoal.RightFoot, _rightFootRot);
    }
    private void FollowHandTo()
    {
        if (!_handRFollow || !_handLFollow) return;
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

        _animator.SetIKPosition(AvatarIKGoal.RightHand, _handRFollow.position);
        _animator.SetIKRotation(AvatarIKGoal.RightHand, _handRFollow.rotation);

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

        _animator.SetIKPosition(AvatarIKGoal.LeftHand, _handLFollow.position);
        _animator.SetIKRotation(AvatarIKGoal.LeftHand, _handLFollow.rotation);
    }

}
