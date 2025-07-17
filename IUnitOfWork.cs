using System;

namespace BranaOS.Opus.Core;

public interface IUnitOfWork
{
  public Task SaveChangesAsync();
}
