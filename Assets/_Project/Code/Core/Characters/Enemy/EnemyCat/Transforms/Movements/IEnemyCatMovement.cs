public interface IEnemyCatMovement: IUpdatable
{
    public enum TypeMovement  { RUN, WALK, WHAIT }
    public TypeMovement typeMovement {set;}
}
