﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RLH.QueryParameters.Entities;
using RLH.Results;

namespace RLH.QueryParameters.ASPNETCore.Extensions
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Adds ErrorDictionary values into a ModelStateDictionary
        /// </summary>
        /// <param name="modelstate"></param>
        /// <param name="errorDictionary"></param>
        public static void AddQueryParameterErrors(this ModelStateDictionary modelstate,Dictionary<string,string> validationErrors)
        {
            foreach (var error in validationErrors)
            {
                modelstate.AddModelError(error.Key, error.Value);
            }
        }
    }
}
