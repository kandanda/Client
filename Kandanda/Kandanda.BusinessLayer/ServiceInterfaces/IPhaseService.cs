﻿using System.Collections.Generic;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IPhaseService
    {
        Phase CreateEmpty();
        Phase GetPhaseById(int id);
        void Update(Phase phase);
        List<Phase> GetAllPhases();
    }
}