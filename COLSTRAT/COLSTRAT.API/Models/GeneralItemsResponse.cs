﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COLSTRAT.API.Models
{
    public class GeneralItemsResponse
    {
        public int GeneralItemId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}