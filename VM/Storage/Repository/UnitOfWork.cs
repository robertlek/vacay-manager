﻿using VM.Storage.DataAccess;
using VM.Storage.Repository.IRepository;

namespace VM.Storage.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;

    public UnitOfWork(Context context)
    {
        _context = context;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
