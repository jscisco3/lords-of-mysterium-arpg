using Godot;
using System;

public partial class WorldScreen : Node2D
{
    private bool _dragging = false;
    private Vector2 _dragStartPosition = Vector2.Zero;
    private RectangleShape2D _selectRect = new();

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Left } mouseClickEvent)
        {
            if (@event.IsPressed())
            {
                _dragging = true;
                _dragStartPosition = mouseClickEvent.Position;
            }
            else if (_dragging)
            {
                _dragging = false;
                QueueRedraw();
                Vector2 _dragEndPosition = mouseClickEvent.Position;
                _selectRect.Size = (_dragEndPosition - _dragStartPosition).Abs();
                var space = GetWorld2D().DirectSpaceState;
                var q = new PhysicsShapeQueryParameters2D();

                q.Shape = _selectRect;
                q.CollisionMask = 2;
                q.Transform = new Transform2D(0, (_dragEndPosition + _dragStartPosition) / 2);
            }
        }

        if (@event is InputEventMouseMotion mouseMotionEvent && _dragging)
        {
            QueueRedraw();
        }
    }

    public override void _Draw()
    {
        if (_dragging)
        {
            DrawRect(new Rect2(
                    _dragStartPosition, GetGlobalMousePosition() - _dragStartPosition), Colors.Aqua, false,
                2.0f);
        }
    }
}