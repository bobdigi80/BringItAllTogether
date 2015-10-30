using System;

namespace BringingItAllTogether.Core
{
  public abstract class BaseEntity
    {
      public Int64 Id { get; set; }
      public DateTime AddedDate { get; set; }
      public DateTime ModifiedDate { get; set; }
      public string IP { get; set; }
    }
}
