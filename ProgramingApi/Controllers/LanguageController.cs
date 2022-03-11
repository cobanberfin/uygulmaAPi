using DataAcessLAyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProgramingApi.Controllers
{
    public class LanguageController : ApiController
    {
        Languagedal ln = new Languagedal();
        //public HttpResponseMessage Get()
        //{
        //    var language= ln.GetAll();
        //    return Request.CreateResponse(HttpStatusCode.OK,language);
        //}
        [ResponseType(typeof(IEnumerable<Language>))]
        public IHttpActionResult Get()
        {
            var languages = ln.GetAll();
            return Ok(languages);
        }
        #region eskı hal
        //public HttpResponseMessage Get(int id)
        //{
        //    // return ln.GetById(id);
        //    var languages = ln.GetById(id);
        //    if (languages == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound, "böyle bir kayıt bulunamadı");
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, languages);

        //}
        #endregion

        public IHttpActionResult Get(int id)
        {
            try
            {
                var lan = ln.GetById(id);

                if (lan == null)
                {
                    return NotFound();
                }
                return Ok(lan);
            }
            catch(Exception e)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage(HttpStatusCode.BadGateway);
                errorResponse.ReasonPhrase = e.Message;
                throw new HttpResponseException(errorResponse);
            }
        }
        public IHttpActionResult Post(Language language)
        {
            if (ModelState.IsValid)
            {
                var createdln = ln.Create(language);
                //return Request.CreateResponse(HttpStatusCode.Created, createdln);
                return CreatedAtRoute("DefaultApi", new { id = createdln.ID }, createdln);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #region eski hal
        public HttpResponseMessage Putt(int id, Language language)
        { //id ye ait kayıt yoksa
            if (ln.IsThereAnyLanguage(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //valıdasyon kuraları doğrulanmadıysa

            else if (ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            //ok
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ln.UpdateLanguage(id, language));
            }

        }
        #endregion
        public IHttpActionResult Put(int id, Language language)
        { //id ye ait kayıt yoksa
            if (ln.IsThereAnyLanguage(id) == false)
            {
                return NotFound();
            }
            //valıdasyon kuraları doğrulanmadıysa

            else if (ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            //ok
            else
            {
                return Ok( ln.UpdateLanguage(id, language));
            }

        }
        public IHttpActionResult Delete(int id)
        {
            if (ln.IsThereAnyLanguage(id) == false)
            {
                return NotFound();
            }
            else
            {
               ln.DeleteLanguage(id);
                return Ok();
            }
           

        }
    }
}
