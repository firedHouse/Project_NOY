//모든 상태가 가져야 할 인터페이스
public interface IBattleState
{
    //상태 진입 시 1회 실행
    void Enter(BattleManager bm);
    //Update에서 실행(감시/로직수행)
    void Execute(BattleManager bm);
    //상태 종료 시 1회 실행
    void Exit(BattleManager bm);    
}
