# PersonalProject03
 
# Ch.03 개인과제 Readme

---

유니티 게임개발 입문 강의를 바탕으로 개인과제 기능 구현에 관한 문서입니다.

## 개인과제에 대하여

---

- Unity를 이용해 게더를 모방을 하는 과제
- 타일맵을 이용해 맵을 구성, 플레이어 에셋을 가지고 플레이어를 조작

## 주요기능

---

### 1.  캐릭터 만들기

- 사전에 제공된 에셋을 package manager를 통해 import 한 후, 플레이어 오브젝트에 적용

![스크린샷 2023-11-29 오전 10.35.05.png](https://prod-files-secure.s3.us-west-2.amazonaws.com/ff3cb08f-147c-449e-bae5-8cfa582ec04d/762607be-42ef-45d0-9a3e-0d06f47262ab/%E1%84%89%E1%85%B3%E1%84%8F%E1%85%B3%E1%84%85%E1%85%B5%E1%86%AB%E1%84%89%E1%85%A3%E1%86%BA_2023-11-29_%E1%84%8B%E1%85%A9%E1%84%8C%E1%85%A5%E1%86%AB_10.35.05.png)

### 2. 캐릭터 이동

- Input System 패키지를 다운 받아, 입력 행동을 쉽게 정의하고, 처리할 수 있는 에셋을 추가
- 스크립트를 작성해 플레이어의 입력 값을 받아 플레이어 오브젝트를 움직이는 코드들을 추가

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if(newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }
}
```

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownCharacterController _controller;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMovement(_movementDirection);
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * 5;

        _rigidbody.velocity = direction;
    }
}
```

### 3. 방 만들기

- 타일맵 오브젝트들을 생성하여, 에셋들로 받은 스프라이트 이미지들을 가져다 2D 공간에 배경들을 구성

![스크린샷 2023-11-29 오전 11.19.11.png](https://prod-files-secure.s3.us-west-2.amazonaws.com/ff3cb08f-147c-449e-bae5-8cfa582ec04d/facd7e28-78a7-43db-8ab8-95a1317afa01/%E1%84%89%E1%85%B3%E1%84%8F%E1%85%B3%E1%84%85%E1%85%B5%E1%86%AB%E1%84%89%E1%85%A3%E1%86%BA_2023-11-29_%E1%84%8B%E1%85%A9%E1%84%8C%E1%85%A5%E1%86%AB_11.19.11.png)

### 4. 카메라 따라가기

- 간단히 카메라 오브젝트를 플레이어의 하위 오브젝트로 연결 시키면 부모 오브젝트의 transition 값에 따라서 카메라는 이동함

![스크린샷 2023-11-29 오전 11.20.11.png](https://prod-files-secure.s3.us-west-2.amazonaws.com/ff3cb08f-147c-449e-bae5-8cfa582ec04d/e2eac86d-362f-4594-90f7-c739cbc78095/%E1%84%89%E1%85%B3%E1%84%8F%E1%85%B3%E1%84%85%E1%85%B5%E1%86%AB%E1%84%89%E1%85%A3%E1%86%BA_2023-11-29_%E1%84%8B%E1%85%A9%E1%84%8C%E1%85%A5%E1%86%AB_11.20.11.png)
