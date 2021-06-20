using UnityEngine;
using System.Linq;
public class Player : MonoBehaviour
{
    #region Unity Variables
    [HideInInspector] public new Rigidbody2D rigidbody2D;
    [HideInInspector] public Animator animator;
    [HideInInspector] public new Transform transform;
    [HideInInspector] public new Rigidbody2D rigidbody;
    [HideInInspector] public new Renderer renderer;
    #endregion

    #region InGame Variables
    [HideInInspector] public bool InMovement = false;
    [HideInInspector] public bool Stucked = false;
    [HideInInspector] MathRNG objMathRNG = new MathRNG(517643879);
    [HideInInspector] public double SpeedObject = 0;
    #endregion

    #region Editor Variables
    public GameManager GameManager;
    public ParticleSystem ParticleLaunch;
    public ParticleSystem ParticleBurst;
    public ParticleSystem ParticleBling;
    public int RotationXMin = 180;
    public int SpeedMovement = 15;
    public double DecreaseSpeed = 0.2;
    public double IncreaseSpeed = 0.8;
    #endregion

    #region General
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        SpeedObject = 0;
        var lstParticle = this.gameObject.GetComponentsInChildren<ParticleSystem>();
        var lstLaunch = (from x in lstParticle where x.gameObject.name == ParticleLaunch.gameObject.name select x).ToList();
        ParticleLaunch = lstLaunch.Count > 0 ? lstLaunch[0] : ParticleLaunch;
        var lstBurst = (from x in lstParticle where x.gameObject.name == ParticleBurst.gameObject.name select x).ToList();
        ParticleBurst = lstBurst.Count > 0 ? lstBurst[0] : ParticleBurst;
        var lstBling = (from x in lstParticle where x.gameObject.name == ParticleBling.gameObject.name select x).ToList();
        ParticleBling = lstBling.Count > 0 ? lstBling[0] : ParticleBling;
    }
    void Update()
    {
        if (GameManager.StartGame && !GameManager.PauseGame)
        {
            animator.SetBool("Death", false);
            var MinX = GameManager.minValues.x;
            var MinY = GameManager.minValues.y;
            var MaxX = GameManager.maxValues.x;
            var MaxY = GameManager.maxValues.y;
            var RenderWidth = (renderer.bounds.size.x / 2);
            var RenderHeight = (renderer.bounds.size.y / 2);
            var transformX = Mathf.Clamp(transform.position.x, MinX + RenderWidth, MaxX - RenderWidth);
            var transformY = Mathf.Clamp(transform.position.y, MinY + RenderHeight, MaxY - RenderHeight);
            Stucked = Stucked == false ? !(transformX == transform.position.x && transformY == transform.position.y) : Stucked;
            transform.position = new Vector3(transformX, transformY, transform.position.z);
            if (!Input.GetKey("space") || GameManager.BlockKeyBoard || GameManager.GameOver)
            {
                Stucked = false;
                animator.SetBool("Run", false);
                if(SpeedObject <= 0)
                {
                    transform.Rotate(0, 0, -(RotationXMin * Time.deltaTime));
                }
                SetAcelerate(-DecreaseSpeed);
                InMovement = false;
                if (ParticleLaunch.isPlaying)
                {
                    ParticleLaunch.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
            else
            {
                if(!ParticleLaunch.isPlaying)
                {
                    ParticleLaunch.Play(true);
                }
                animator.SetBool("Run", true);
                if (!InMovement)
                {
                    InMovement = true;
                }
                SetAcelerate(IncreaseSpeed);
            }
            if(GameManager.GameOver)
            {
                setSpeed(0);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.StartGame)
        {
            if (collision.gameObject.tag == "Star")
            {
                ParticleBling.Play();
                collision.gameObject.transform.position = new Vector3(objMathRNG.NextValueFloat(-9, 9), objMathRNG.NextValueFloat(-5, 5), 0);
                GameManager.Score += 1;
            }
            if (collision.gameObject.tag == "Obstaculo" && !GameManager.GameOver)
            {
                animator.SetBool("Death", true);
                GameManager.GameOver = true;
                ParticleBurst.Play();
                renderer.enabled = false;
                setSpeed(0);
            }
        }
    }
    #endregion

    #region Set Speed
    public void setSpeed(float Speed)
    {
        Vector2 newVelocity;
        var angle = rigidbody.rotation;
        newVelocity.x = Mathf.Cos(angle * Mathf.Deg2Rad) * Speed;
        newVelocity.y = Mathf.Sin(angle * Mathf.Deg2Rad) * Speed;
        newVelocity.x = Mathf.Round(newVelocity.x);
        newVelocity.y = Mathf.Round(newVelocity.y);
        if(Stucked)
        {
            newVelocity.x = 0;
            newVelocity.y = 0;
        }
        rigidbody.velocity = newVelocity;
    }
    #endregion

    #region Aceelerate
    public void SetAcelerate(double Speed)
    {
        Vector2 newVelocity;
        var angle = rigidbody.rotation;
        newVelocity.x = Mathf.Cos(angle * Mathf.Deg2Rad) * (float)SpeedObject;
        newVelocity.y = Mathf.Sin(angle * Mathf.Deg2Rad) * (float)SpeedObject;
        newVelocity.x = Mathf.Round(newVelocity.x);
        newVelocity.y = Mathf.Round(newVelocity.y);
        if (Stucked)
        {
            newVelocity.x = 0;
            newVelocity.y = 0;
            SpeedObject = 0;
        }
        rigidbody.velocity = newVelocity;
        SpeedObject += Speed;
        SpeedObject = SpeedObject >= SpeedMovement ? SpeedMovement : SpeedObject;
        SpeedObject = SpeedObject <= 0 ? 0 : SpeedObject;
    }
    #endregion
}
#region OLD CODES
//[HideInInspector] public float FraccAngle = 360/8;
//var angles = (transform.rotation.z * Mathf.Rad2Deg) * Mathf.PI;
//angles = Mathf.Round(angles / FraccAngle) * FraccAngle;
//rigidbody.rotation = angles;
//if (!Input.GetKey("space") || GameManager.BlockKeyBoard)
//{
//    Stucked = false;
//    transform.Rotate(0, 0, -(RotationXMin * Time.deltaTime));
//    setSpeed(0);
//    InMovement = false;
//    animator.SetBool("Run", false);
//}
//else
//{
//    animator.SetBool("Run", true);
//    if (!InMovement)
//    {
//        InMovement = true;
//    }
//    setSpeed(SpeedMovement);
//}
#endregion