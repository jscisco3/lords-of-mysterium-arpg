using Godot;
using System;

public partial class Character : Node2D
{
    private Logger logger;
    private AnimationPlayer _animationPlayer;

    [Export] public float Speed = 1.0f;
    
    public override void _Ready()
    {
        logger = new Logger(this);
        _animationPlayer = GetNode<AnimationPlayer>("Sprite2D/AnimationPlayer");
        _animationPlayer.Play("walking");
        logger.info("Speed: " + Speed);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        if (direction != Vector2.Zero)
        {
            logger.info(direction.ToString());
        }
        Vector2 velocity = direction * Speed * (float) delta;
        if (velocity != Vector2.Zero)
        {
            logger.info(velocity.ToString());
        }
        Position += velocity;
    }
}
