using MODS.PTW.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODS.PTW.Models
{
    public class Company
    {
        //public Company()
        //{
        //    this.companyCatagories = new HashSet<CompanyCatagory>();
        //}
       
        public int ID { get; set; }
        //[ForeignKey("CompanyCatagory")]
        public int CategoryID { get; set; }
        public string Name { get; set; }               
        public string  Code { get; set; }
        public virtual CompanyCatagory companyCatagories { get; set; }


    }
}
