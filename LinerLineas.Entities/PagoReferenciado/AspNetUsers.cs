using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities.PagoReferenciado;

namespace LinerLineas.Entities.PagoReferenciado
{
    public class AspNetUsers
    {
        //Todos los strings son de tipo nvarchar
        public string sId { get; set; }
        public string sEmail { get; set; }
        public bool  bEmailConfirmed { get; set; }
        public string sPasswordHash { get; set; }
        public string sSecurityStamp { get; set; }
        public string sPhoneNumber { get; set; }
        public bool bPhoneNumberConfirmed { get; set; }
        public bool bTwoFactorEnabled { get; set; }
        public DateTime daLockoutEndDateUtc { get; set; }
        public bool bLockoutEnabled { get; set; }
        public int nAccessFailedCount { get; set; }
        public string sUserName { get; set; }
        public bool bIsNewUser { get; set; }
        public string sNombre { get; set; }
        public string sApellidoP { get; set; }
        public string sApellidoM { get; set; }
        public string sRFC { get; set; }
        public bool besMoral { get; set; }

        //Propiedades navegacion
        public AspNetUsersRoles rASPNET_USERS_ROLES { get; set; }
        public LineasUsuario rLINEAS_USUARIO { get; set; }
        public ClientesUsuarios rCLIENTES_USUARIOS { get; set; }
    }
}
