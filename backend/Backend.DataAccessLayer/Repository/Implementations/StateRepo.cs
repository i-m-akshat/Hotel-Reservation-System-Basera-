﻿using Backend.DataAccessLayer.Context.DBContext;
using Backend.DataAccessLayer.Context.Models;
using Backend.DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccessLayer.Repository.Implementations
{
    public class StateRepo : IStateRepo
    {
        private readonly BaseraHotelReservationSystemContext _context;
        public StateRepo(BaseraHotelReservationSystemContext context)
        {
            _context = context;
        }
        public async Task<TblState> Create(TblState tblState)
        {
            await _context.TblStates.AddAsync(tblState);
            await _context.SaveChangesAsync();
            return tblState;
        }

        public Task<TblState> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<TblState> Get(long id)
        {
            return await _context.TblStates.FindAsync(id);
        }

        public async Task<List<TblState>> GetAll()
            {
            return await  _context.TblStates.Include(x=>x.Country).ToListAsync();
        }

        public Task<TblState> Update(TblState tblState)
        {
            throw new NotImplementedException();
        }
    }
    
}
