using System;
using HFramework;

namespace Game.Procedure
{
    public class GameMaster : SingletonAutoMono<GameMaster>
    {
        private bool _isInit = false;
        public bool IsInit => _isInit;
        
        private void Start()
        {
            HEntry.Init();
            HEntry.ProcedureMgr.Initialize(new InitProcedure(),
                new BeginProcedure(), new RomaProcedure());
            HEntry.ProcedureMgr.StartProcedure<InitProcedure>();

            _isInit = true;
        }

    }
}