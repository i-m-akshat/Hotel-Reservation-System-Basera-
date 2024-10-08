﻿using Backend.Models.City_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface ICityService
    {

        /// <summary>
        /// To Get all the cities 
        /// </summary>
        /// <returns></returns>
        List<City> Get();
        /// <summary>
        /// To Get the city by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        City GetById(int id);
        void Create(City city);
        void Update(City city);
        void Delete(int id);
    }
}
