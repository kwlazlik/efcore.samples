using System;

namespace ConsoleApp1.Domain
{
   public class PackageDocumentationTypeInfo : DocumentationTypeInfo<PackageDocumentationTypeInfo>
   {
      public static PackageDocumentationTypeInfo New(DocumentationType type, DateTime from, DateTime to) => new PackageDocumentationTypeInfo
      {
         Type = type,
         ValidFrom = from,
         ValidTo = to
      };
   }
}
