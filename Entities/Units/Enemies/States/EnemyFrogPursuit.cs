using Godot;
using System;

[GlobalClass]
public partial class EnemyFrogPursuit : EnemyState
{
    [Export]
    private string pursitAniamtionJumpUp = "JumpUp";
    [Export]
    private string pursitAniamtionJumpDown = "JumpDown";


    protected static float Speed = 100.0f;
    private float jumpForce = -300.0f;

    private float dirX = 0.0f;


    public override void Enter()
    {
        owner.animationPlayer.Play(pursitAniamtionJumpUp);
        GD.Print($"{owner.Name} pursuit state");

        dirX = 0.0f;
    }

    public override void PhysicsUpdate(double delta)
    {
        Vector2 vel = CalculateGravity(owner.Velocity, delta);
        bool isInSight = true;
        if (owner.IsOnFloor())
        {
            if (owner.pdc.IsPlayerInSight())
            {
                vel = Jump(vel);

                Vector2 direction = owner.pdc.GetDirToPlayer();

                if (direction.X < 0.0f)
                {
                    dirX = -1.0f;
                    owner.FlipH(false);
                }
                else
                {
                    dirX = 1.0f;
                    owner.FlipH(true);
                }
            }
            else
            {
                isInSight = false;
            }
        }
        else
        {
            vel.X = Speed * dirX;
        }

        PlayAnim(vel.Y);

        owner.Velocity = vel;
        owner.MoveAndSlide();


        if (!isInSight)
        {
            stateMachine.ChangeState("Idle");
        }
    }

    private Vector2 Jump(Vector2 vel)
    {
        vel.Y = jumpForce;
        return vel;
    }

    private void PlayAnim(float y)
    {
        if (y < 0)
        {
            owner.animationPlayer.Play(pursitAniamtionJumpUp);
        }
        else
        {
            owner.animationPlayer.Play(pursitAniamtionJumpDown);
        }
    }
}
