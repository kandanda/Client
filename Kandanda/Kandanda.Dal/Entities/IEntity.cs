﻿namespace Kandanda.Dal.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        byte[] RowVersion { get; set; }
    }
}