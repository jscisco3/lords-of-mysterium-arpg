using Godot;
using System;

public partial class Character : Node2D
{
    private Logger logger;
    private Sprite2D _sprite2D;
    private AnimationPlayer _animationPlayer;

    [Export] public float Speed = 1.0f;
    
    private bool _facingRight = true;
    private bool _moving = false;
    
    public override void _Ready()
    {
        logger = new Logger(this);
        _sprite2D = GetNode<Sprite2D>("Sprite2D");
        _animationPlayer = GetNode<AnimationPlayer>("Sprite2D/AnimationPlayer");
        _animationPlayer.Play("idle");
        logger.info("Speed: " + Speed);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        _moving = direction != Vector2.Zero;
        _animationPlayer.Play(_moving ? "idle" : "walking");
        _facingRight = !(direction.X < 0);
        if (_moving)
        {
            _sprite2D.FlipH = !_facingRight;
        }
        Vector2 velocity = direction * Speed * (float) delta;
        Position += velocity;
    }
}
