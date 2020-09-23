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
    [Route("movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public IDbConnection GetConnection()
        {
            return new SqlConnection("Server=GQJSN13\\SQLEXPRESS;Database=movielist;user id=newuser;password=abc123");
        }

        [HttpGet]
        public List<Movies> ReadAll()
        {
            IDbConnection db = GetConnection();
            List<Movies> movieList = db.GetAll<Movies>().ToList();
            return movieList;
        }

        [HttpGet("{category}")]
        public List<Movies> movieCategory(string category)
        {
            IDbConnection db = GetConnection();

            List<Movies> categoryList = db.Query<Movies>($"select * from movies where category = '{category}'").AsList();
            
            return categoryList;
        }
        [HttpGet("listcategory")]
        public List<MovieCategory> listCategory()
        {
            IDbConnection db = GetConnection();

            List<MovieCategory> categoryList = db.Query<MovieCategory>($"select distinct category from movies").AsList();

            return categoryList;
        }

        [HttpGet("random")]
        public Movies randomMovie()
        {
            IDbConnection db = GetConnection();
            List<Movies> movieList = db.GetAll<Movies>().ToList();

            return movieList[new Random().Next(movieList.Count)];
        }
        /*
        [HttpGet("random{category}")]
        public Movies randomMovieCategory(string category)
        {
            IDbConnection db = GetConnection();
            List<Movies> categoryList = db.Query<Movies>($"select * from movies where category like '{category}'").AsList();

            //what happens if the category is empty?
            return categoryList[new Random().Next(categoryList.Count)];
        }
        */
        [HttpGet("random{category}")]
        public JsonResult randomMovieCategory(string category)
        {
            IDbConnection db = GetConnection();
            List<Movies> categoryList = db.Query<Movies>($"select * from movies where category like '{category}'").AsList();

            if(categoryList.Count <=0)
            {
                return new JsonResult(new { });
            }
            else
            {
                return new JsonResult(categoryList[new Random().Next(categoryList.Count)]);
            }


        }
        [HttpGet("search{moviename}")]
        public List<Movies> movieTitle(string movieName)
        {
            IDbConnection db = GetConnection();

            List<Movies> categoryList = db.Query<Movies>($"select * from movies where moviename like '%{movieName}%'").AsList();

            return categoryList;
        }
    }
}
