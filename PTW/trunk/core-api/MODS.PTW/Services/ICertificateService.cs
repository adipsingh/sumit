using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface ICertificateService
    {
        Certificate Create(Certificate certificate);
        Certificate Update(Certificate certificate, int id);
        Certificate Delete(int id);
        List<Certificate> GetAllCertificate();
        Certificate GetById(int id);
    }
}
