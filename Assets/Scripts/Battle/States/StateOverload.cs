using Unity.VisualScripting;
using UnityEngine;


public class StateOverload : IBattleState
{
    
    public void Enter(BattleManager battleManager)
    { }

    public void Execute(BattleManager battleManager)
    {
        ApplyOverload(battleManager);

        ResetTurnFlags(battleManager);

        if(CheckWinLoss(battleManager))
        {
            return;
        }

        battleManager.ChangeState(new StatePlayerTurn());
    }

    public void Exit(BattleManager battleManager)
    { }

    private void ApplyOverload(BattleManager bm)
    {
        bool hasOverload = false;

        foreach(var unit in bm.EnemyTeam)
        {
            var elemental = unit.GetComponent<ElementalManager>();
            if(elemental != null && elemental.IsOverloadActive)
            {
                hasOverload = true;
                break;
            }
        }

        if(!hasOverload)
        { return; }

        ReactionDamageProcesser.ApplyOverload(bm.EnemyTeam);
            
        foreach(var unit in bm.EnemyTeam)
        {
            unit.GetComponent<ElementalManager>()?.ConsumeOverload();
        }

    }

    private void ResetTurnFlags(BattleManager bm)
    {
        foreach(var unit in bm.PlayerTeam)
        {
            unit.GetComponent<ElementalManager>()?.ResetTurn();
        }
        foreach(var unit in bm.EnemyTeam)
        {
            unit.GetComponent<ElementalManager>()?.ResetTurn();
        }
    }
    //½ÂÆÐÁ¶°ÇÃ¼Å©
    private bool CheckWinLoss(BattleManager bm)
    {
        //Àû Àü¸ê -> ½Â¸®
        if (bm.EnemyTeam.Count == 0)
        {
            bm.ChangeState(new StateEnd(true));
            return true;
        }
        //¾Æ±º Àü¸ê -> ÆÐ¹è
        if (bm.PlayerTeam.Count == 0)
        {
            bm.ChangeState(new StateEnd(false));
            return true;
        }
        return false;
    }
}
