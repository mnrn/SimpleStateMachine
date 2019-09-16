using LanguageExt;
using static LanguageExt.Prelude;

public class StateMachine<T> {
  private Option<T> owner_;
  private Option<State<T>> currentState_;
  private Option<State<T>> previousState_;
  private Option<State<T>> globalState_;

  public StateMachine (T owner, State<T> initState) {
    owner_ = owner;
    currentState_ = initState;
  }

  public void ChangeState (State<T> nextState) {
    previousState_ = currentState_;
    ifSome (from state in currentState_ from owner in owner_ select (state: state, owner: owner),
      t => t.state.Exit (t.owner));
    currentState_ = nextState;
    ifSome (from state in currentState_ from owner in owner_ select (state: state, owner: owner),
      t => t.state.Enter (t.owner));
  }

  public void Update () {
    List (currentState_, globalState_)
      .Map (s => ifSome (
        from state in s from owner in owner_ select (state: state, owner: owner),
        t => t.state.Execute (t.owner)
      ));
  }

  public void SetGlobalState(State<T> nextGlobalState) {
    globalState_ = nextGlobalState;
  }
}