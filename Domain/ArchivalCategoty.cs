namespace Domain
{
   public class ArchivalCategoty : Entity<ArchivalCategoty>
   {
      public ArchivalCategoty(int years)
      {
         Years = years;
      }

      public int Years { get; private set; }
   }
}
