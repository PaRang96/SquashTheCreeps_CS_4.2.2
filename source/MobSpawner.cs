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
        base._Ready();
        MobTimer.WaitTime = 0.5f;
        MobTimer.Autostart = true;
        MobTimer.Timeout += SpawnMob;

        GetNode<Player>("Player").Hit += OnPlayerDies;
    }

    private void OnPlayerDies()
    {
        MobTimer.Stop();

    }

    private void SpawnMob()
    {
        Mob mob = MobScene.Instantiate<Mob>();

        PathFollow3D mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");

        mobSpawnLocation.ProgressRatio = GD.Randf();
        Player player = GetNode<Player>("Player");
        if (player != null)
        {
            Vector3 playerPosition = player.Position;
            mob.Initialize(mobSpawnLocation.Position, playerPosition);
            AddChild(mob);
        }
    }
}
