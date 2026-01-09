using Godot;
using System;

public partial class Unit : CharacterBody2D
{
    private Logger logger;
    private Sprite2D _sprite2D;
    private AnimationPlayer _animationPlayer;

    [Export] public float Speed = 1.0f;

    private Vector2? _target = null;
    
    private bool _facingRight = true;
    private bool _moving = false;
    
    public override void _Ready()
    {
        logger = new Logger(this);
        _sprite2D = GetNode<Sprite2D>("Sprite2D");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _animationPlayer.Play("idle");
        logger.info("Speed: " + Speed);
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("move_to_target"))
        {
            _target = GetGlobalMousePosition();
            logger.info("Moving to target...");
        }
        if (@event.IsActionPressed("move_up"))
        {
            var distance = _target != null ? Position.DistanceTo(_target.Value) : -1000000.0f;
            logger.info("Distance to target: " + distance);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_target != null)
        {
            Velocity = Position.DirectionTo(_target.Value);
        }
        Velocity = Velocity.Normalized() *  Speed;
        MoveAndCollide(Velocity * (float) delta);
        if (_target != null)
        {
            if (Position.DistanceTo(_target.Value) < 1.2f)
            {
                _target = null;
                Velocity = Vector2.Zero;
            }   
        }
    }
}
