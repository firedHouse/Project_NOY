using Unity.VisualScripting;
using UnityEngine;


public class StateOverload : IBattleState
{
    private const int Duration = 3;
    private int remainTurn = 0;
    private bool isActive = false;

    public void Activate()
    {
        if(isActive)
        {
            return;
        }
        isActive = true;
        remainTurn = Duration;
        Debug.Log("과부하 진행");
    }


    public void Enter(BattleManager battleManager)
    {

    }

    public void Execute(BattleManager battleManager)
    {
        if(isActive)
        {
            ReactionDamageProcesser.Overload(battleManager.EnemyTeam);

            remainTurn--;
            Debug.Log($"과부하 {remainTurn}턴 남음");

            if(remainTurn <= 0)
            {
                isActive = false;
                Debug.Log("과부하 종료");
            }
        }

        battleManager.ChangeState(new StateOrderCalculation());
    }

    public void Exit(BattleManager battleManager)
    { }
}
