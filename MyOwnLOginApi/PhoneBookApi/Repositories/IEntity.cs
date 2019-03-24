using System;

namespace PhoneBookApi.Repositories
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}