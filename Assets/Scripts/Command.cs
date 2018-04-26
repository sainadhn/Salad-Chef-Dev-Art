using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public Player player;
    public virtual void Execute() { } // CALLED when key is pressed..
    public virtual void OnKeyReleased()
    {
        player.OnAnyKeyReleased();
    }
    //public abstract void MoveForward();
    //public abstract void MoveDown();
    //public abstract void MoveLeft();
    //public abstract void MoveRight();
    //public abstract void PickItem();
    //public abstract void DropItem();
    //public abstract void Chop();
    //public abstract void ThrowInTrash();
    //public abstract void Serve();
}

public class MoveForward : Command
{
    public MoveForward(Player p)
    {
        player = p;
    }
    public override void Execute()
    {
        player.MoveForward();
    }
}

public class MoveDown : Command
{
    public MoveDown(Player p)
    {
        player = p;
    }
    public override void Execute()
    {
        player.MoveDown();
    }
}

public class MoveLeft : Command
{
    public MoveLeft(Player p)
    {
        player = p;
    }
    public override void Execute()
    {
        player.MoveLeft();
    }
}

public class MoveRight : Command
{
    public MoveRight(Player p)
    {
        player = p;
    }
    public override void Execute()
    {
        player.MoveRight();
    }
}

public class PickItem : Command
{
    public PickItem(Player p)
    {
        player = p;
    }
    public override void Execute( )
    {
        player.PickItem();
    }
}

public class DropItem : Command
{
    public DropItem(Player p)
    {
        player = p;
    }
    public override void Execute( )
    {
        player.DropItem();
    }
}

public class Chop : Command
{
    public Chop(Player p)
    {
        player = p;
    }
    public override void Execute( )
    {
        player.Chop();
    }
}

public class Serve: Command
{
    public Serve(Player p)
    {
        player = p;
    }
    public override void Execute( )
    {
        player.Serve();
    }
}

public class ThrowInTrash : Command
{
    public ThrowInTrash(Player p)
    {
        player = p;
    }
    public override void Execute( )
    {
        player.ThrowInTrash();
    }
}