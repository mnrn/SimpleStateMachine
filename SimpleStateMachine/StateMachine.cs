using LanguageExt;
using static LanguageExt.Prelude;

namespace StateMachine {
    public class StateMachine<T> {
        private Option<T> owner_;
        private Option<State<T>> currentState_;
        private Option<State<T>> previousState_;

        StateMachine (T owner) {
            owner_ = Some (owner);
        }

        public void ChangeState (State<T> nextState) {
            previousState_ = currentState_;
            currentState_.IfSome (state => state.Exit (owner_));
            currentState_ = Some (nextState);
            currentState_.IfSome (state => state.Enter (owner_));
        }
        public void Update () {
            currentState_.IfSome (state => state.Execute (owner_));
        }
    }
}