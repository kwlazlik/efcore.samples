using System.Collections.Generic;
using DelegateDecompiler;

namespace Domain
{
   public class Package : Entity<Package>
   {
      private readonly List<PackageDocumentationTypeInfo> _documentationTypeInfos = new List<PackageDocumentationTypeInfo>();
      private string _test;

      public PackageNumber Number { get; private set; }

      public PackageStatus Status { get; private set; }

      public string Test
      {
         get => _test;
         set => _test = value;
      }

      public IReadOnlyList<PackageDocumentationTypeInfo> DocumentationTypeInfos => _documentationTypeInfos;

      [Computed] public string NumberStatus => Number.Value + Status.Value;

      public void Send(PackageNumber number)
      {
         Number = number;
         Status = PackageStatus.Sent;
      }

      public void AddDocumentationTypeInfo(PackageDocumentationTypeInfo info)
      {
         _documentationTypeInfos.Add(info);
      }

      public static Package Create(PackageNumber number, PackageStatus status) => new Package
      {
         Number = number,
         Status = status
      };
   }
}
