using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace lab15._2_Rest_Api.Controllers
{
    [Table("movies")]
    public class Movies
    {
        [Key]
        public int ID { get; set; }
        public string MovieName { get; set; }
        public string Category { get; set; }
    }
}
