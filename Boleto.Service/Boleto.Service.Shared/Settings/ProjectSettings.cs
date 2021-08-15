namespace Boleto.Service.Shared.Settings
{
    public class ProjectSettings
    {
        public DataBaseBacen DataBaseBacen { get; set; }
    }
    public class DataBaseBacen
    {
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
    }
}
