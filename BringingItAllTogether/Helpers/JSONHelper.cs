﻿using System.Net;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections.Generic;
using System;
using BringingItAllTogether.ErrorHelper;

namespace BringingItAllTogether.Helpers
{
    public static class JSONHelper
    {
        #region Public extension methods.
        /// <summary>
        /// Extened method of object class, Converts an object to a json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                return serializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                 throw new ApiBusinessException(1002, ex.Message, HttpStatusCode.NoContent);
            }
        }
        #endregion
    }
}