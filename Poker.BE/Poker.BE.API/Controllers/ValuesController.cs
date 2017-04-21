using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poker.BE.API.Controllers
{
    public class ValuesController : ApiController
    {
        #region Fields
        private ICollection<string> StringTests =
            new HashSet<string>(new string[] { "value1", "value2" });
        #endregion

        #region Methods
        // GET api/values
        public IEnumerable<string> Get()
        {
            return StringTests;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return StringTests.ElementAt(id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            StringTests.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            StringTests.Remove(StringTests.ElementAt(id));
            StringTests.Add(value);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            StringTests.Remove(StringTests.ElementAt(id));
        }
        #endregion
    }
}
