namespace Domain
{
   public interface ISingleValueValueType<out T>
   {
      T Value { get; }
   }
}
