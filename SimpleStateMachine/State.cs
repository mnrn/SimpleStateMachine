using LanguageExt;

namespace StateMachine {
    public abstract class State<T> {
        public abstract void Enter (Option<T> t);
        public abstract void Exit (Option<T> t);
        public abstract void Execute (Option<T> t);
    }
}