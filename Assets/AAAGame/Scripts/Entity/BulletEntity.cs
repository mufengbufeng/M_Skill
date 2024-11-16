using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEntity : EntityBase
{
    public const string LIFE_TIME = "LifeTime";
    private float moveSpeed = 50f;

    Rigidbody m_body;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        m_body = GetComponent<Rigidbody>();
    }
    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
#if UNITY_6000_0_OR_NEWER
        m_body.linearVelocity = transform.forward * moveSpeed;
#else
        m_body.velocity = transform.forward * moveSpeed;
#endif
        float lifeTime = Params.Get<VarFloat>(LIFE_TIME);
        UniTask.Delay(TimeSpan.FromSeconds(lifeTime)).ContinueWith(LifeTimeOver).Forget();
    }

    private void LifeTimeOver()
    {
        GF.Entity.HideEntity(this.Entity);
    }
}
