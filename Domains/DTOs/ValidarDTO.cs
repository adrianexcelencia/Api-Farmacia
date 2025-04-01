namespace APIformbuilder.Domains.DTOs
{
    public class ValidarDTO
    {
      public string token { get; set; }
        public string usuario { get; set; }

        public int  id_usuario { get; set; }

        public  List<int> permisos { get; set; }

        public int  rol { get; set; }
        public string mensaje { get; set; }

    }
}
