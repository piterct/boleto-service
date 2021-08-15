namespace Boleto.Service.Shared.Settings
{
    public class ProjectSettings
    {
        public DataBaseBacenSettings DataBaseBacenSettings { get; set; }
    }
    public class DataBaseBacenSettings
    {
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
    }
}
