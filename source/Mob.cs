using Godot;
using System;

public partial class Mob : CharacterBody3D
{
    [Export]
    public int MinSpeed { get; set; } = 10;

    [Export]
    public int MaxSpeed { get; set; } = 18;

    [Export]
    public VisibleOnScreenNotifier3D visibilityNotifier { get; set; }

    [Signal]
    public delegate void SquashedEventHandler();

    // This function will be called from the main scene
    public void Initialize(Vector3 startPosition, Vector3 playerPosition)
    {
        LookAtFromPosition(startPosition, playerPosition, Vector3.Up);

        RotateY((float)GD.RandRange(-MathF.PI / 4.0f, MathF.PI / 4.0f));

        int randomSpeed = GD.RandRange(MinSpeed, MaxSpeed);
        Velocity = Vector3.Forward * randomSpeed;
        Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);

        visibilityNotifier.ScreenExited += OnVisibilityNotifierScreenExited;
    }

    private void OnVisibilityNotifierScreenExited()
    {
        QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

    public void Squash()
    {
        EmitSignal(SignalName.Squashed);

        QueueFree();
    }
}
