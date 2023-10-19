using System.Threading;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game;

public class TestCasts : Script
{

    public UIControl TestX;
    public EmptyActor Source;
    public CharacterController Target;

    private float _timer = 0f;

    public override void OnStart()
    {
        Screen.CursorLock = CursorLockMode.None;
        Screen.CursorVisible = true;
        (TestX.Control as Button).Clicked += PerformTest;
    }

    public override void OnFixedUpdate()
    {
        _timer += Time.DeltaTime;
        if (_timer >= 0.125f)
        {
            _timer = 0;
            PerformTest();
        }
    }

    private void PerformTest()
    {
        DebugDraw.DrawLine(Source.Position, Source.Position + (Target.Position - Source.Position) * 1000f, Color.Blue, 0.125f);
        if (Physics.RayCastAll(
            Source.Position,
            Target.Position - Source.Position,
            out RayCastHit[] hits,
            layerMask: 1 | 1 << 1,
            hitTriggers: false
        ))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                DebugDraw.DrawSphere(new BoundingSphere(hits[i].Point, 10), Color.Red, 0.125f, false);
                DebugDraw.DrawText($"{i}: {hits[i].Collider.Name}", hits[i].Point, Color.Red, duration: 0.125f);
            }
        }
    }
}

