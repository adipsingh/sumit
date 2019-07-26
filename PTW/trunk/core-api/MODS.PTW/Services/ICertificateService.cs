using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface ICertificateService
    {
        Certificate create(Certificate certificate);
        Certificate update(Certificate certificate, int id);
        Certificate Delete(int id);
        List<Certificate> GetAllCertificate();
        Certificate GetById(int id);
    }
}
