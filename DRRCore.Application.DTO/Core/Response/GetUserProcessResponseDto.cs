namespace DRRCore.Application.DTO.Core.Response
{
    public class GetUserProcessResponseDto
    {
        public List<UserProcessDto>? Gerencia { get; set; }
        public List<UserProcessDto>? Produccion { get; set; }
        public List<UserProcessDto>? Administracion { get; set; }
    }
    public class UserProcessDto
    {
        public int Id { get; set; }
        public int IdProcess { get; set; }
        public int IdUser { get; set; }
        public bool Enable { get; set; }
    }
}
