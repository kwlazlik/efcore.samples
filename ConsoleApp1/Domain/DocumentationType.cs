namespace ConsoleApp2.Domain
{
   public class DocumentationType : Entity<DocumentationType>
   {
      private DocumentationType(string name)
      {
         Name = name;
      }

      public DocumentationType(string name, ArchivalCategoty archivalCategory) : this(name)
      {
         ArchivalCategory = archivalCategory;
      }

      public string Name { get; }

      public ArchivalCategoty ArchivalCategory { get; }
   }
}
