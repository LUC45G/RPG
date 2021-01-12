using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Variables")]
    [Space]
    [SerializeField] private float speed;

    [Header("Components")] 
    [Space] 
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] private Rigidbody2D myRb;
    [SerializeField] private Animator myAnimator;

    private Transform myTransform;
    private bool _canMove;
    private Coroutine _movementCoroutine;
    
    private void Awake()
    {
        myTransform = transform;
        _canMove = true;
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (!_canMove) return;
        
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        if (ShouldMove(x))
        {
            Move((int)Mathf.Sign(x), Vector3.right);
            mySpriteRenderer.flipX = x < 0;
        }
        else if (ShouldMove(y)) 
            Move((int)Mathf.Sign(y), Vector3.up);
        else
            myAnimator.SetBool("Walking", false);
        
    }
    
    private bool ShouldMove(float x)
    {
        return Mathf.Abs(x) > .0025f;
    }

    public Collider2D LookingTarget
    {
        get
        {
            var current = myTransform.position;
            //Debug.DrawRay(current, _lastMovement * gridSnap, Color.blue);
            //var atSnap = Physics2D.Raycast(current, _lastMovement, .5f).collider;
            /*var atDouble =*/ return Physics2D.Raycast(current, _lastMovement, .75f).collider;
            //return atSnap ? atSnap : atDouble;
        }
    }

    private Vector2 _lastMovement;
    
    private void Move(int sign, Vector3 dir)
    {
        var current = myTransform.position;
        var actualPosition = current;
        var target = actualPosition + Time.fixedDeltaTime * speed * sign * dir;
        _lastMovement = dir * sign;
        myRb.MovePosition(target);
        myAnimator.SetBool("Walking", true);
        
        //_canMove = false;
        //_movementCoroutine = StartCoroutine(MoveTowards(target));
    }
    

    private IEnumerator MoveTowards(Vector3 target)
    {
        var current = myRb.position;
        var initial = current;
        var t = 0f;
        myAnimator.SetBool("Walking", true);
        
        while (Vector2.Distance(current, target) > 0.01f)
        {
            t += Time.fixedDeltaTime;
            myRb.MovePosition(Vector2.Lerp(current, target, t));
            current = myRb.position;
            yield return new WaitForEndOfFrame();
            if (t > .75f && !_canMove) _canMove = true; 
        }
        
        myRb.position = Round(current);
        myAnimator.SetBool("Walking", false);
        _canMove = true;
        _movementCoroutine = null;
    }

    public void ForceMovement(Vector3 position)
    {
        if(_movementCoroutine != null) StopCoroutine(_movementCoroutine);
        
        _movementCoroutine = null;
        myAnimator.SetBool("Walking", false);
        _canMove = true;
        
        myTransform.position = Round(position);
    }

    private Vector3 Round(Vector3 vec)
    {
        var x = vec.x;
        var y = vec.y;
        var v = Vector3.zero;

        var fdx = Mathf.Abs((int) (x * 10)) % 10;
        var fdy = Mathf.Abs((int) (y * 10)) % 10;
        
        x = IsInRange(fdx, 3f, 7f) ? Mathf.FloorToInt(x) + .5f : Mathf.RoundToInt(x);
        y = IsInRange(fdy, 3f, 7f) ? Mathf.FloorToInt(y) + .5f : Mathf.RoundToInt(y);

        v.x = x;
        v.y = y;
        v.z = vec.z;
        return v;
    }

    private bool IsInRange(float value, float min, float max) => value >= min && value <= max;
}
