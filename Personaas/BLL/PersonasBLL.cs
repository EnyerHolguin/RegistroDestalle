using Microsoft.EntityFrameworkCore;
using Personaas.DAL;
using Personaas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Personaas.BLL
{
    public class PersonasBLL
    {
        public static bool Guardar(Personas persona)
        {
            bool paso = false;
            Context db = new Context();
            try
            {
                if (db.Personas.Add(persona) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }


            return paso;
        }

        public static bool Modificar(Personas persona)
        {
            bool paso = false;
            Context db = new Context();
            try
            {
                var Anterior = db.Personas.Find(persona.PersonaId);
                foreach (var item in Anterior.Telefono)
                {
                    if (persona.Telefono.Exists(d => d.Id == item.Id))
                        db.Entry(item).State = EntityState.Modified;
                }
                db.Entry(persona).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Context db = new Context();
            try
            {
                var Eliminar = db.Personas.Find(id);
                db.Entry(Eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static Personas Buscar(int id)
        {
            Personas persona = new Personas();
            Context db = new Context();

            try
            {
                persona = db.Personas.Find(id);

                persona.Telefono.Count();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return persona;
        }
        public static List<Personas> GetList(Expression<Func<Personas, bool>> persona
            )
        {
            List<Personas> Lista = new List<Personas>();
            Context db = new Context();

            try
            {
                Lista = db.Personas.Where(persona).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }

    }
}
