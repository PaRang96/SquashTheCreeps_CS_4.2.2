using Godot;
using System;

public partial class MobSpawner : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	[Export]
	public Timer MobTimer { get; set; }

    public override void _Ready()
    {
        MobTimer.WaitTime = 0.5f;
        MobTimer.Autostart = true;
        MobTimer.Timeout += SpawnMob;
    }

    private void SpawnMob()
    {
        Mob mob = MobScene.Instantiate<Mob>();

        PathFollow3D mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");

        mobSpawnLocation.ProgressRatio = GD.Randf();
        Vector3 playerPosition = GetNode<Player>("Player").Position;
        mob.Initialize(mobSpawnLocation.Position, playerPosition);

        AddChild(mob);
    }
}
