using StudentAPIw6.Model;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.DTOs.Response;

namespace StudentAPIw6.AutoMapper
{
    public static class BoMonMapper
    {
        // Entity -> Response DTO
        public static BoMonResponseDTO ToResponse(this BoMon boMon)
        {
            return new BoMonResponseDTO
            {
                id = boMon.id,
                tenMon = boMon.maBM,
                maBM = boMon.tenBM
            };
        }

        // List Entity -> List Response DTO
        public static List<BoMonResponseDTO> ToResponseList(this IEnumerable<BoMon> boMons)
        {
            return boMons.Select(x => x.ToResponse()).ToList();
        }

        // Create DTO -> Entity
        public static BoMon ToEntity(this BoMonRequestDTO.CreateBoMonDTO dto)
        {
            return new BoMon
            {
                tenBM = dto.tenMon
            };
        }


        public static void updateEntity(this BoMon boMon, BoMonRequestDTO.UpdateBoMonDTO dto)
        {
            boMon.tenBM = dto.tenMon;
        }
    }
}