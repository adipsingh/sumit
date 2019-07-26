using Microsoft.AspNetCore.Http;
using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Services
{
    public interface IImageService
    {
        string Upload(string Type,string Path,string Name,DateTime uploadFile);
        Image GetImage(int id);
    }
}
