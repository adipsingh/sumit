﻿using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface IQuestionnarieService
    {
        Questionnarie Create(Questionnarie questionnarie); 
    }
}
