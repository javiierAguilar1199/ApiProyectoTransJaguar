using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataLayer.EntityModel
{

    public class catalogOrganizacion

    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pIdOrganzacion { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pNombre { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pTelefono { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pDireccion { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pIdUsuario { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pTransaccionEstado { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pTransaccionMensaje { get; set; }
    }
    public class catalogoUsrRol

    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pIdRol { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pDescripcioRol { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pIdUsuario { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pTransaccionEstado { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pTransaccionMensaje { get; set; }
    }

    public class catalogoInfogeneral
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pDescripcion { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pCantidad { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
       
        public int pTransaccionEstado { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pTransaccionMensaje { get; set; }
    }

        public class catalogoMarca
    {


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pIdMarca { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string pMarca { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pTransaccionEstado { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? pTransaccionMensaje { get; set; }
    }
    public class cataloRoles
    {


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pIdRol { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string pDescripcion { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pTransaccionEstado { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? pTransaccionMensaje { get; set; }
    }
    public class CatalogoEntityDepto
    {


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pIdDepartamento { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string pNombre { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string pTransaccionMensaje { get; set; }

    }
    public class CatalogoEntityDeptoXmun
    {


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pNombre_Departamento { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string pId_Municipio { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int pNombre { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string pTransaccionMensaje { get; set; }
    }

    public class CatalogoEntityMunicipio
    {

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? pDepartamento { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? pNombre { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pidDepartamento { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pidMunicipio { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pTransaccionEstado { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? pTransaccionMensaje { get; set; }

    }


    public class cantidadViajeDPto

    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pTransaccionEstado { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? pTransaccionMensaje { get; set; }
       
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string pNombre { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int pCantidad { get; set; }
    }


}
