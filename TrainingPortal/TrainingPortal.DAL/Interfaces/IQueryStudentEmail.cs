﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingPortal.DAL.Interfaces
{
    public interface IQueryStudentEmail
    {
        bool IsEmailRegistered(string EmailID);

    }
}