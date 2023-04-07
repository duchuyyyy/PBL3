using MyOToVer1._2.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace MyOToVer1._2.Models.ViewModels
{
    public class CarOwnerViewModels
    {
        public Car Car { get; set; }
        public Owner Owner { get; set; }
    }
}
