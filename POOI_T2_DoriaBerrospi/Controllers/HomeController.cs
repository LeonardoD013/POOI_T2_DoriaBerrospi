using Newtonsoft.Json;
using POOI_T2_DoriaBerrospi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POOI_T2_DoriaBerrospi.Controllers
{
    public class HomeController : Controller
    {

        static string jAlumno = @"[]";
        public ActionResult Index()
        {
            try
            {
                List<Alumno> listAlumno = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);
                return View(listAlumno);
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                return View();
            }

        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(Alumno alumno)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);

                if (temporal.Any(a => a.dni == alumno.dni))
                {
                    ViewBag.mensaje = "El DNI se encuentra registrado";
                    return View(alumno);
                }
                
                temporal.Add(alumno);
                jAlumno = JsonConvert.SerializeObject(temporal);
                ViewBag.mensaje = "Guardado de Manera Exitosa";
                return View(alumno);
            }
            catch (JsonException ex)
            {
                ViewBag.mensaje = ex.Message;
                return View();
            }
        }

        public ActionResult Eliminar(string dni)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);
                Alumno alumno = temporal.Find(a=> a.dni == dni);

                if (alumno != null)
                {
                    temporal.Remove(alumno);
                    jAlumno = JsonConvert.SerializeObject(temporal);
                }
            }
            catch(JsonException ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Detalles(string dni)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);
                Alumno alumno = temporal.Find(a => a.dni == dni);
                return View(alumno);
            }
            catch (JsonException ex)
            {
                ViewBag.mensaje = ex.Message;
                return View();
            }
        }

        public ActionResult Actualizar(string dni)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);
                Alumno alumnoEncontrado = temporal.Find(a => a.dni == dni);
                return View(alumnoEncontrado);
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Actualizar(Alumno alumno)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);
                int index = temporal.FindIndex(a => a.dni == alumno.dni);

                if (index == -1)
                {
                    temporal.Add(alumno);
                    jAlumno = JsonConvert.SerializeObject(temporal);
                    ViewBag.mensaje = "El DNI no existe, se Agrego al Alumno";
                    return View(alumno);
                }

                temporal[index] = alumno;
                jAlumno = JsonConvert.SerializeObject(temporal);
                ViewBag.mensaje = "Alumno actualizado";
                return View(alumno);
            }
            catch (JsonException ex)
            {
                ViewBag.mensaje = ex.Message;
                return View(alumno);
            }
        }

        /*public ActionResult DeserializacionAlumno(string dni)
        {
            string mensaje = string.Empty;
            if (dni == null) return View();
            try
            {
                Alumno alumnoLocal = JsonConvert.DeserializeObject<Alumno>(jAlumno);
                return View(alumnoLocal);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            ViewBag.mensaje = mensaje;
            return View();

        }

        public ActionResult SerializacionAlumno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SerializacionAlumno(Alumno alumno)
        {
            string mensaje = string.Empty;
            try
            {
                jAlumno = JsonConvert.SerializeObject(alumno);
                mensaje = "Serializado de manera correcta";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            ViewBag.mensaje = mensaje;
            return View(alumno);
        }*/
    }
}