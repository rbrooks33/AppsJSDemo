using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppsJSClassLibStandard
{
    public class PeopleController : ApiController
    {
        public class Result
        {
            public Result()
            {
                Logs = new List<string>();
            }
            public bool Success { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
            public List<string> Logs { get; set; }
        }

        public class MyCustomPerson
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Route("api/AppsJS/MyCustomPeopleApi")]
        [HttpGet]
        public Result MyCustomPeopleAPi(string name)
        {
            var result = new Result();

            try
            {
                var people = new List<MyCustomPerson>();

                var newPerson = new MyCustomPerson();
                newPerson.FirstName = "Joe";
                newPerson.LastName = "Schmoe";
                people.Add(newPerson);

                var newPerson2 = new MyCustomPerson();
                newPerson2.FirstName = "Fred";
                newPerson2.LastName = "Freling";
                people.Add(newPerson2);

                result.Data = people;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Data = ex;
            }
            return result;
        }
    }
}
