namespace Domain
{
   public class DocumentationType : Entity<DocumentationType>
   {
      public DocumentationType()
      {
         
      }

      public DocumentationType(string name, ArchivalCategoty archivalCategory)
      {
         Name = name;
         ArchivalCategory = archivalCategory;
      }

      public string Name { get; private set; }

      public ArchivalCategoty ArchivalCategory { get; private set; }
   }
}
