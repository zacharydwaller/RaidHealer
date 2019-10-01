using System;

public interface ICastEventHandler
{
    void OnStartingCast(object sender, CastEventArgs e);
    void OnCancellingCast(object sender, CastEventArgs e);
    void OnFinishingCast(object sender, CastEventArgs e);
}

public class CastEventArgs : EventArgs
{
    public BattleManager Mgr { get; set; }
    public Entity Owner { get; set; }
    public Ability Ability { get; set; }
}