using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossBehaviour : EnemyBehavior
{
    public enum BossBehaviour { 
        Line,
        Sine,
        X,
        Break
    }

    [Header("Boss Settings")]
    public Transform projectileOrigin;

    public BossBehaviour bossBehaviour;

    [SerializeField]
    int numberAtks;
    [SerializeField]
    int countAtks;
    [SerializeField]
    bool isReadyToInstantiate = true;
    [SerializeField]
    float shootDelay;
    [SerializeField]
    bool isShootingLeft = false;

    float maxAngle, minAngle, actualAngle;

    bool isReadyToChangeState;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReadyToChangeState && doAtk == false)
        {
            StartCoroutine("ChangeBehaviour");
        }
        if (doAtk == false && bossBehaviour != BossBehaviour.Break)
            Shoot();
        if (doAtk == true && countAtks < numberAtks && isReadyToInstantiate == true) {
            switch (bossBehaviour)
            {
                case BossBehaviour.Line:
                    StartCoroutine("InstantiatePrefabs");
                    break;
                case BossBehaviour.Sine:
                    float angleRotation = 20;
                    if (isShootingLeft == false) {
                        actualAngle += angleRotation;
                        if (actualAngle >= maxAngle)
                            isShootingLeft = true;
                    }
                    else{
                        actualAngle -= angleRotation;
                        if (actualAngle <= minAngle)
                            isShootingLeft = false;
                    }
                    actualAngle = Mathf.Clamp(actualAngle, minAngle, maxAngle);
                    projectileOrigin.rotation = Quaternion.AngleAxis(actualAngle, Vector3.forward);
                    StartCoroutine("InstantiatePrefabs");
                    break;
                case BossBehaviour.X:
                    actualAngle += 20;
                    StartCoroutine(InstantiatePrefabs(4));
                    break;
                case BossBehaviour.Break:
                    break;
                default:
                    break;
            }
        }
        if(countAtks >= numberAtks && doAtk == true)
        {
            doAtk = false;
            countAtks = 0;
            bossBehaviour = BossBehaviour.Break;
            isReadyToChangeState = true;
        }
    }

    IEnumerator ChangeBehaviour() {
        isReadyToChangeState = false;
        float wait = Random.Range(5,8);
        bossBehaviour = BossBehaviour.Break;
        yield return new WaitForSeconds(wait);
        bossBehaviour = (BossBehaviour)Random.Range(0,3);
    }

    void Shoot() {
        doAtk = true;
        lastTargetPosition = target.position;
        numberAtks = Random.Range(5, 20);

        switch (bossBehaviour)
        {
            case BossBehaviour.Break:

                break;
            case BossBehaviour.Line:
                ShootLine();
                break;
            case BossBehaviour.Sine:
                numberAtks = 40;
                ShootSine();
                break;
            case BossBehaviour.X:
                ShootX();
                break;
            default:

                break;
        }
    }

    void ShootLine() {
        Vector2 atkDir = lastTargetPosition - transform.position;
        atkDir.Normalize();
        float angle = Mathf.Atan2(atkDir.y, atkDir.x) * Mathf.Rad2Deg;
        projectileOrigin.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Debug.DrawRay(transform.position, atkDir, Color.red);
    }

    void ShootSine() {
        Vector2 atkDir = lastTargetPosition - transform.position;
        atkDir.Normalize();
        float angle = Mathf.Atan2(atkDir.y, atkDir.x) * Mathf.Rad2Deg;
        actualAngle = angle;
        maxAngle = angle + 45;
        minAngle = angle - 45;
        projectileOrigin.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void ShootX() {
        Vector2 atkDir = lastTargetPosition - transform.position;
        atkDir.Normalize();
        float angle = Mathf.Atan2(atkDir.y, atkDir.x) * Mathf.Rad2Deg;
        actualAngle = angle;
        projectileOrigin.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator InstantiatePrefabs() {
        isReadyToInstantiate = false;
        Instantiate(prefabRangedAttack, projectileOrigin.position, projectileOrigin.rotation);
        countAtks++;
        yield return new WaitForSeconds(shootDelay);
        isReadyToInstantiate = true;
    }

    IEnumerator InstantiatePrefabs(int qtd)
    {
        isReadyToInstantiate = false;
        float rotz = actualAngle;
        for (int i = 0; i < qtd; i++)
        {
            Vector3 projectileRot = new Vector3(projectileOrigin.position.x, 
                projectileOrigin.position.y, 
                projectileOrigin.position.z + rotz);
            Instantiate(prefabRangedAttack, projectileOrigin.position, Quaternion.Euler(projectileRot));
            rotz += 90;
        }
        countAtks++;
        yield return new WaitForSeconds(shootDelay);
        isReadyToInstantiate = true;
    }

}
