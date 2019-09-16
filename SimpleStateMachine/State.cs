using LanguageExt;

public abstract class State<T> {
  public virtual void Enter(T ix) {
  }
  public virtual void Exit(T ix) {
  }
  public abstract void Execute(T ix);
}