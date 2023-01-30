using System;
using Hali_Framework;

namespace Game.Procedure
{
    public class GameMaster : SingletonMono<GameMaster>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            ProcedureMgr.Instance.Initialize(FsmMgr.Instance,
                new InitProcedure());
        }

        private void Start()
        {
            ProcedureMgr.Instance.StartProcedure<InitProcedure>();
        }
    }
}