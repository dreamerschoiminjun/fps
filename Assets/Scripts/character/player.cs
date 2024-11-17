using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]                                                       // ����Ƽ ���ο��� Ȯ��
    public Transform characterBody;                                        // �ش�Ǵ� ��ü�� �巡�� �� �����ϸ� �°� ���
    public Transform cameraArm;                                            // �ش�Ǵ� ��ü�� �巡�� �� �����ϸ� �°� ���
    public float applySpeed;                                               // ����Ǵ� �ӵ��� ������ ����
    public CharacterStats characterStats;                                  // ĳ���� ���ȿ��� �ʿ��� �� �޾ƿ��� ���� ����

    public float WaterHeight = 15.5f;                                      // ���� ���� ����
    private bool isInWater = false;                                        // ĳ���Ͱ� ���� �ִ��� ���� Ȯ�� ����
    private float originalSpeed;                                           // ������ �̵� �ӵ� ����
    private float originalDrag;                                            // ������ Drag �� ����
    private float waterDrag = 5f;                                          // �� �ӿ����� Drag ��

    bool jump;                                                             // ���� ���� Ȯ�� ����
    bool isJump;                                                           // �������� ���� �����ϰ� �ϴ� ����
    

    float hAxis;                                                           // Ű�� �ޱ����� ����
    float vAxis;                                                           // Ű�� �ޱ����� ����

    bool isBorder;                                                         // ������ ���� ����    

    Vector3 moveVec;                                                       // ���� ������ ���� ����
    Rigidbody rigid;                                                       // ����ȿ�� ����
    Animator anim;                                                         // �ִϸ��̼� �ֱ� ���� ����

    // �Ѿ� ������ �迭 �߰�
    public GameObject[] BulletPrefabs;                                     // ���� ������ �Ѿ� ������

    // ���Ÿ� ���� ���� �Լ��� �ʿ��� ����
    public GameObject BulletPrefab;                                        // �߻�ü ������
    public Transform gunTransform;                                         // �߻� ��ġ�� ����� Transform
    private bool isFireReady;                                              // �߻� �غ� ����
    bool attack;                                                           // ���� ���� Ȯ�� ����

    void Start()
    {
        rigid = GetComponent<Rigidbody>();                                 // Rigidbody ������Ʈ ��������
        originalDrag = rigid.drag;                                         // ������ Drag �� ����

        anim = characterBody.GetComponent<Animator>();                     // Animator ������Ʈ ��������

        // �ִϸ����ÿ� ����ϱ� ���� ���� ����
        anim.SetBool("isWalk", false);                                     // �ȱ� ���� �ʱ�ȭ
        anim.SetBool("isStrafeRight", false);                              // ���������� ��Ʈ������ ���� �ʱ�ȭ
        anim.SetBool("isStrafeLeft", false);                               // �������� ��Ʈ������ ���� �ʱ�ȭ
        anim.SetBool("isWalkBack", false);                                 // �ڷ� �ȱ� ���� �ʱ�ȭ
        applySpeed = characterStats.MoveSpeed;                             // �̵� �ӵ� �ʱ�ȭ
        originalSpeed = applySpeed;                                        // ������ �̵� �ӵ� ����
    }

    void Update()
    {
        CheckForWaterHeight();                                             // �� ���� üũ
        GetInput();                                                        // �Է� �� �ޱ�
        Aim();                                                             // ���� ��� ȣ��
        Move();                                                            // �̵� ��� ȣ��
        Jump();                                                            // ���� ��� ȣ��
        BulletAttack();                                                    // ���Ÿ� ���� ��� ȣ��
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");                            // ���� �Է� �� �ޱ�
        vAxis = Input.GetAxisRaw("Vertical");                              // ���� �Է� �� �ޱ�
    }

    void Aim()
    {
        // ���콺 ������ ���� �޾ƿɴϴ�.
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // ī�޶��� ���� ������ �޾ƿɴϴ�.
        Vector3 camAngle = cameraArm.localEulerAngles;

        // ���콺�� Y�� �̵��� ���� ī�޶��� X�� ȸ���� ����մϴ�.
        float x = camAngle.x - mouseDelta.y;

        // X�� ȸ���� -45���� 45�� ���̷� �����մϴ�.
        if (x > 180f) x -= 360f; // 360�� �� ������ �߻��� �� �ִ� ������ ����
        x = Mathf.Clamp(x, -45f, 45f);

        // ī�޶��� ȸ���� �����մϴ�.
        cameraArm.localEulerAngles = new Vector3(x, camAngle.y, 0);

        // ���콺�� X�� �̵��� ���� ĳ������ Y�� ȸ���� ����Ͽ� �����մϴ�.
        float yRotation = transform.eulerAngles.y + mouseDelta.x;
        transform.eulerAngles = new Vector3(0, yRotation, 0);
    }


    void Move() // tps ĳ���� ������ ����
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;                                          // �̵� ���� ���

        if (Input.GetKey(KeyCode.LeftShift) && !isInWater && !attack)
        {

            applySpeed = characterStats.SprintSpeed;                                                // ������Ʈ �ӵ� ����
            anim.speed = 2.0f;                                                                      // �޸� �� �ִϸ��̼� �ӵ��� 2��� ����
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || isInWater)
        {

            applySpeed = originalSpeed;                                                             // ���� �ӵ� ����
            anim.speed = 1.0f;                                                                      // �ִϸ��̼� �ӵ��� �������� ����
        }

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));    // �̵� �Է� �� �ޱ�
        bool isMove = moveInput.magnitude != 0;                                                     // �̵� ���� Ȯ��
        anim.SetBool("isWalk", vAxis > 0);                                                          // vAxis�� ����� ���� isWalk�� true�� ����

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized;  // ���� ���� ���
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0, cameraArm.right.z).normalized;        // ������ ���� ���
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;                      // �̵� ���� ���

            characterBody.forward = lookForward;                                                    // ĳ������ ������ ����
            if (!isBorder)
                transform.position += moveDir * Time.deltaTime * applySpeed;                        // �̵� ����
            UnityEngine.Debug.DrawRay(transform.position, lookForward, Color.green);                 // �̵� ���� �����
            isBorder = Physics.Raycast(transform.position, lookForward, 1, LayerMask.GetMask("Wall")); // ������ �浹 ����

            // ��Ʈ������ ���� �߰�
            if (hAxis > 0)
            {
                anim.SetBool("isStrafeRight", true);                                                // ���������� ��Ʈ������ �ִϸ��̼� ����
                anim.SetBool("isStrafeLeft", false);
                anim.SetBool("isWalkBack", false);
            }
            else if (hAxis < 0)
            {
                anim.SetBool("isStrafeRight", false);
                anim.SetBool("isStrafeLeft", true);                                                 // �������� ��Ʈ������ �ִϸ��̼� ����
                anim.SetBool("isWalkBack", false);
            }
            else if (vAxis < 0)
            {
                anim.SetBool("isStrafeRight", false);
                anim.SetBool("isStrafeLeft", false);
                anim.SetBool("isWalkBack", true);                                                   // �ڷ� �ȱ� �ִϸ��̼� ����
            }
            else
            {
                anim.SetBool("isStrafeRight", false);
                anim.SetBool("isStrafeLeft", false);
                anim.SetBool("isWalkBack", false);
            }
        }
        else
        {
            anim.SetBool("isStrafeRight", false);
            anim.SetBool("isStrafeLeft", false);
            anim.SetBool("isWalkBack", false);
        }
    }

    void Jump()
    {
        jump = Input.GetButtonDown("Jump");                                                         // ���� Ű �Է� Ȯ��
        if ( !attack && jump && !isJump)
        {
            if (vAxis < 0)
            {
                anim.SetTrigger("isJumpBackward");                                                  // �ڷ� ���� �ִϸ��̼� Ʈ����
            }
            else
            {
                anim.SetTrigger("isJumpForward");                                                   // ������ ���� �ִϸ��̼� Ʈ����
            }
            rigid.AddForce(Vector3.up * characterStats.jumppower, ForceMode.Impulse);               // ���� �� ����
            anim.SetBool("isJump", true);                                                           // ���� ���� ����
            anim.SetTrigger("doJump");                                                              // ���� �ִϸ��̼� Ʈ����
            isJump = true;                                                                          // ���� �� ���� ����
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground") // �ٴڰ��� �浹 Ȯ��
        {
            isJump = false; // ���� ���� ����
            anim.SetBool("isJump", false); // ���� �ִϸ��̼� ����


        }
    }

    void BulletAttack()
    {
        bool isMouseDown = Input.GetMouseButton(0);  // ���콺 ���� ��ư �Է� Ȯ�� (0�� ���� ��ư)
        bool isMouseUp = Input.GetMouseButtonUp(0);  // ���콺 ���� ��ư�� ������ ���� Ȯ��
        characterStats.fireDelay += Time.deltaTime;  // �߻� ���� �ð� ���� (�����Ӵ� ��� �ð� �߰�)
        isFireReady = characterStats.fireDelay >= characterStats.bulletfireRate;  // �߻� �غ� ���� �Ǵ� (���� �ð��� �߻� �ӵ����� ū�� Ȯ��)

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isInWater && !attack; // �޸��� ���� Ȯ��

        if (isMouseDown && isFireReady && !isRunning)  // �߻� �غ� �Ǿ���, �޸��� ���� �ƴ� ��
        {
            attack = true;  // ���� ���¸� true�� ����
            anim.SetBool("isFiring", true);  // �߻� �ִϸ��̼� ����
            StartCoroutine(Bullet());  // �߻� �ڷ�ƾ ����
            characterStats.fireDelay = 0;  // �߻� ���� �ð� �ʱ�ȭ
        }
        else if (isMouseUp || isRunning)  // ���콺 ��ư�� ������ ��� �Ǵ� �޸��� ���� ���
        {
            attack = false;  // ���� ���¸� false�� ����
            anim.SetBool("isFiring", false);  // �߻� �ִϸ��̼� ����
        }
    }



    IEnumerator Bullet()
    {
        ShootProjectile();                                                                         // �߻�ü �߻�
        yield return new WaitForSeconds(characterStats.bulletfireRate);                            // �߻� �ӵ���ŭ ���
        isFireReady = true;                                                                        // �߻� �غ� �Ϸ�
    }

    void ShootProjectile()
    {
        Ray ray = new Ray(cameraArm.position, cameraArm.forward);                                  // ī�޶� ��ġ���� �������� ����(Ray) ����
        RaycastHit hit;                                                                            // RaycastHit ����ü ���� (�浹 ���� ����)
        Vector3 targetPoint;                                                                       // ��ǥ ���� ���� ����

        if (Physics.Raycast(ray, out hit))                                                         // ������ ���𰡿� �浹�ߴ��� Ȯ��
        {
            targetPoint = hit.point;                                                               // ������ �浹�� ���� ����
        }
        else
        {
            targetPoint = cameraArm.position + cameraArm.forward * 100f;                           // �浹���� ������ ������ �� �Ÿ� ����
        }

        Vector3 direction = (targetPoint - gunTransform.position).normalized;                      // �߻� ���� ��� (��ǥ �������� �ѱ� ��ġ������ ����)
        GameObject projectile = Instantiate(BulletPrefab, gunTransform.position, Quaternion.LookRotation(direction)); // �߻�ü ���� (�ѱ� ��ġ����)
        projectile.transform.forward = direction;                                                  // �߻�ü�� ���� ���� ����
        projectile.GetComponent<Rigidbody>().AddForce(direction * characterStats.bulletspeed);     // �߻�ü�� ���� ���Ͽ� �߻�
    }


    void CheckForWaterHeight()
    {
        if (transform.position.y < WaterHeight)
        {
            isInWater = true;                                                                      // ĳ���Ͱ� ���� ������ ǥ��
            rigid.useGravity = false;                                                              // ���� ���� �� �߷� ��Ȱ��ȭ
            rigid.drag = waterDrag;                                                                // �� �ӿ����� ���� �� ����
            applySpeed = characterStats.MoveSpeed / 2;                                             // ���� ���� �� �̵� �ӵ� ����
        }
        else
        {
            isInWater = false;                                                                     // ĳ���Ͱ� �� �ۿ� ������ ǥ��
            rigid.useGravity = true;                                                               // �� �ۿ� ���� �� �߷� Ȱ��ȭ
            rigid.drag = originalDrag;                                                             // �� �ۿ����� ���� ���� �� ����
            applySpeed = originalSpeed;                                                            // �� �ۿ� ���� �� ���� �ӵ� ����
        }
    }
}
