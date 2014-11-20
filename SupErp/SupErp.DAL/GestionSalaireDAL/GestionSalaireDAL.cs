﻿using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SupErp.DAL.GestionSalaireDAL
{
    public class GestionSalaireDAL
    {
        private SUPERPEntities _entities;
        public SUPERPEntities Entities
        {
            get
            {
                if (_entities == null)
                    _entities = new SUPERPEntities();
                return _entities;
            }
            set
            {
                _entities = value;
            }
        }

        #region PRIMES

        /// <summary>
        /// Retourne une prime en fonction de l'ID en param
        /// </summary>
        /// <param name="id">Id Prime</param>
        /// <returns> une Prime</returns>
        public Prime GetPrimeById(long id)
        {
            return Entities.Primes.Find(id);
        }

        /// <summary>
        /// Retourne une liste de primes pour un user
        /// </summary>
        /// <param name="id">Id user</param>
        /// <returns>Liste de primes</returns>
        public List<Prime> GetPrimesByUserId(long id)
        {
            return (from p in GetPrimes()
                    where p.User_id == id
                    select p).ToList();
        }

        /// <summary>
        /// Retourne l'ensemble des primes
        /// </summary>
        /// <returns>Liste de primes</returns>
        public List<Prime> GetPrimes()
        {
            return Entities.Primes.ToList();
        }
        #endregion

        #region ABSENCES
        /// <summary>
        /// Retourne une absence en fonction de l'ID en param
        /// </summary>
        /// <param name="id">Id Prime</param>
        /// <returns> une Absence</returns>
        public Absence GetAbsenceById(long id)
        {
            return Entities.Absences.Find(id);
        }

        /// <summary>
        /// Retourne une liste d'abscence pour un user
        /// </summary>
        /// <param name="id">Id user</param>
        /// <returns>Liste d'abscence</returns>
        public List<Absence> GetAbsenceByUserId(long id)
        {
            return (from p in GetAbsenses()
                    where p.User_id == id
                    select p).ToList();
        }

        /// <summary>
        /// Retourne l'ensemble des primes
        /// </summary>
        /// <returns>Liste d'absence</returns>
        public List<Absence> GetAbsenses()
        {
            return Entities.Absences.ToList();
        }
        #endregion



        #region USERS


        /// <summary>
        /// Retourne l'ensemble des primes
        /// </summary>
        /// <returns>Liste de primes</returns>
        public List<User> GetUsers(String q)
        {

            if (!String.IsNullOrEmpty(q))
            { 
                //StringBuilder sb = new StringBuilder();
                //bool first = true;
                //foreach (char c in q)
                //{
                //    if (first)
                //    {
                //        sb.Append("(");
                //        first = false;
                //    }
                //    if (c.Equals(' '))
                //    {
                //        sb.Append(")?");
                //        first = true;
                //    }
                //    else
                //    {
                //        sb.Append(c);
                //    }
                //}
                //sb.Append(")?");
                //Regex regex = new Regex(sb.ToString(), RegexOptions.IgnoreCase);
                var query = q.Split(' ');
                return Entities.Users.Include("Salaries").Include("Absences").Where(x => query.Contains(x.Lastname) || query.Contains(x.Firstname) || x.Lastname.Contains(q) || x.Firstname.Contains(q)).ToList();
            }
            return Entities.Users.Include("Salaries").Include("Absences").ToList();
            
        }

        /// <summary>
        /// Retourne l'ensemble des primes
        /// </summary>
        /// <returns>Liste de primes</returns>
        public List<User> GetUsersById(long id)
        {
            return Entities.Users.Include("Salaries").Include("Absences").Include("Primes").Where(x => x.Id == id).ToList();
        }


        #endregion
    }
}
