using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Staff : MonoBehaviour, IWeapon
{
    [Header("Fire/Reload time")]
    // First Coroutine
    private Coroutine _coroutineFiring;
    private WaitForSeconds _waitForFiring;
    [SerializeField] private float _waitTimeFiring = 0.1f;

    // Second Coroutine
    private Coroutine _coroutineReloading;
    private WaitForSeconds _waitForReloading;
    [SerializeField] private float _waitTimeReloading = 0.7f;

    [SerializeField] private Canvas _canvas;

    [Header("Local")]
    [SerializeField] private float _maxDist = 20f;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private int ammo = 12;
    private LineRenderer _lineRenderer;
    private bool _canFire;
    //public List<IDamageable> TargetsHit = new List<IDamageable>();

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

    }
    private void Start()
    {
        _lineRenderer.enabled = false;
        _canFire = true;
        _waitForFiring = new WaitForSeconds(_waitTimeFiring);
        _waitForReloading = new WaitForSeconds(_waitTimeReloading);
    }



    public void Fire()
    {
        if (gameObject.activeSelf == true && _canFire)
        {
            ammo -= 1;
            Vector3 targetPos = FireLogic(transform.position + transform.up * _maxDist);
            FireVisuals(targetPos);

            _coroutineFiring = StartCoroutine(FiringCor());
            _coroutineReloading = StartCoroutine(ReloadingCor());
        }

    }

    public Vector3 FireLogic(Vector3 targetPos)
    {
        
        RaycastHit hitinfo;
        var direction = targetPos - transform.position;

        Debug.DrawRay(transform.position, direction, Color.red, 1f);

        if (Physics.Raycast(transform.position, direction, out hitinfo, _maxDist))
        {
            var targetHit = hitinfo.transform;
            if (targetHit != null)
            {
                //Debug.Log($"FireWeapon.targetHit: {targetHit.name} ");
                //Destroy(targetHit.transform.gameObject);

                targetHit.GetComponent<IDamageable>()?.ReceiveDamage(_damage, targetHit.position);

                return hitinfo.point;
            }

        }
        return targetPos;

    }

    

    public void FireVisuals(Vector3 targetPos)
    {
        Vector3 muzzlePoint = transform.up * 2;
        if (_canFire)
        {
            
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position + muzzlePoint);
            //_lineRenderer.SetPosition(1, transform.position + transform.forward * _maxDist);
            _lineRenderer.SetPosition(1, targetPos);
            _canFire = false;


        }
    }

    private IEnumerator FiringCor()
    {
        yield return _waitForFiring;

        _lineRenderer.enabled = false;
    }
    private IEnumerator ReloadingCor()
    {
        yield return _waitForReloading;

        _canFire = true;

    }





    public ISpell CurrentSpell => throw new System.NotImplementedException();

    IReadOnlyList<ISpell> IWeapon.AvailableSpells => throw new System.NotImplementedException();

    public void Cast()
    {
        
    }
  
    public void SwitchSpell()
    {
        throw new System.NotImplementedException();
    }
}
