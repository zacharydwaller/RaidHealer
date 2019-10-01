using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Like beacon of light
/// </summary>
//public class Attune : AuraAbility
//{
//    public Attune(Entity owner = null)
//        : base(owner)
//    {
//        Name = "Attune";
//        CastAdd = 0f;
//        Cooldown = 4.0f;
//        ImagePath = "Image/Cleric/attune";
//    }

//    public override Aura CreateAura()
//    {
//        return new AttuneBuff(Target as Raider);
//    }
//}

//public class AttuneBuff : AuraEffect
//{

//    public AttuneBuff(Raider owner, Entity applier = null)
//        : base(owner, applier)
//    {
//        Name = "Attune";
//    }

//    public override void Start()
//    {
//        foreach (Raider raider in Owner.Mgr.Raid.Raiders)
//        {
//            foreach (var aura in raider.Auras)
//            {
//                if (aura.GetType() == typeof(AttuneBuff))
//                {
//                    aura.Finish();
//                    break;
//                }
//            }
//        }

//        base.Start();
//    }

//    // Override tick so it doesn't call base.Tick() and never expires
//    public override void Tick()
//    {
//        //base.Tick();
//    }
//}