using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MODS.PTW.Dtos;

namespace MODS.PTW.Services
{
    public interface IMediaService
    {
        Task<string> create(Media media);
        Media update(Media media, int id);
        Task<IActionResult> Upload(Media formFile);
        Media Download(int id);
        Media delete(int id);
    }
}
