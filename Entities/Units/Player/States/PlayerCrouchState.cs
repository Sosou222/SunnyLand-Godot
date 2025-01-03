using Godot;
using System;

[GlobalClass]
public partial class PlayerCrouchState : PlayerMovementState
{
    [Export]
    private CollisionShape2D playerColision;
    [Export]
    private RectangleShape2D playerNormalShape;
    [Export]
    private RectangleShape2D playerCrouchShape;
    [Export]
    private RayCast2D leftRaycast;
    [Export]
    private RayCast2D rightRaycast;
    [Export]
    private string crouchAnimation = "Crouch";

    private Vector2I crouchPosition = new Vector2I(-1, 9);
    private Vector2I normalPosition = new Vector2I(2, 4);


    public override void Enter()
    {
        GD.Print("Crouch Player Enter");
        owner.animationPlayer.Play(crouchAnimation);
        SetShape(playerCrouchShape, crouchPosition);

        Speed = NormalSpeed;
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        if (!WantToCrouch())
        {
            if (!(leftRaycast.IsColliding() || rightRaycast.IsColliding()))
            {
                stateMachine.ChangeState(StateNames.Idle.ToString());
            }
        }
    }

    public override void Exit()
    {
        SetShape(playerNormalShape, normalPosition);
    }

    private void SetShape(RectangleShape2D shape, Vector2I position)
    {
        playerColision.Shape = shape;
        playerColision.Position = position;
    }
}
